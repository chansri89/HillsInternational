/* ==========================================================================
   Hills TIS client UI (no dependencies, ES5).
   Presentation only: no business logic and no server calls of its own.
   - theme (light/dark) and sidebar rail state, persisted in localStorage
   - collapsible nav groups, rail flyouts, mobile drawer
   - command palette (Ctrl/Cmd+K or "/")
   - toasts: window.alert() is replaced so the ~500 server-side
     ScriptManager.RegisterStartupScript("alert(...)") calls become toasts
   - [data-confirm] confirmation dialog
   - progress bar for full and partial (UpdatePanel) postbacks
   - drag-and-drop file zones and table quick filters
   ========================================================================== */
(function () {
    "use strict";

    var doc = document;
    var root = doc.documentElement;
    var ICONS = "Images/icons.svg";

    function store(key, value) {
        try {
            if (value === undefined) return localStorage.getItem(key);
            if (value === null) localStorage.removeItem(key); else localStorage.setItem(key, value);
        } catch (e) { return null; }
        return value;
    }

    function icon(name, cls) {
        return '<svg class="tis-icon' + (cls ? " " + cls : "") + '" aria-hidden="true"><use href="' + ICONS + "#i-" + name + '"></use></svg>';
    }

    function closest(el, selector) {
        while (el && el.nodeType === 1) {
            if (matches(el, selector)) return el;
            el = el.parentNode;
        }
        return null;
    }

    function matches(el, selector) {
        var fn = el.matches || el.msMatchesSelector || el.webkitMatchesSelector;
        return fn ? fn.call(el, selector) : false;
    }

    function each(list, fn) { Array.prototype.forEach.call(list || [], fn); }

    function escapeHtml(text) {
        return String(text).replace(/[&<>"']/g, function (c) {
            return { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c];
        });
    }

    /* ------------------------------------------------------------ theme */

    function setTheme(theme) {
        root.setAttribute("data-theme", theme);
        store("tis.theme", theme);
    }

    function toggleTheme() {
        setTheme(root.getAttribute("data-theme") === "dark" ? "light" : "dark");
    }

    /* -------------------------------------------------------- navigation */

    function isMobile() { return window.matchMedia("(max-width: 1024px)").matches; }

    function toggleRail() {
        var rail = root.getAttribute("data-nav") !== "rail";
        if (rail) root.setAttribute("data-nav", "rail"); else root.removeAttribute("data-nav");
        store("tis.nav", rail ? "rail" : null);
        closeFlyout();
    }

    function openDrawer(open) {
        root.classList.toggle("tis-nav-open", open);
    }

    function openGroups() {
        var saved = [];
        try { saved = JSON.parse(store("tis.navOpen") || "[]"); } catch (e) { saved = []; }
        return saved;
    }

    function saveGroups() {
        var keys = [];
        each(doc.querySelectorAll(".tis-nav__group.is-open"), function (g) {
            if (!g.classList.contains("has-active")) keys.push(g.getAttribute("data-group"));
        });
        store("tis.navOpen", JSON.stringify(keys));
    }

    function initNav() {
        var saved = openGroups();
        each(doc.querySelectorAll(".tis-nav__group"), function (g) {
            if (g.classList.contains("has-active") || saved.indexOf(g.getAttribute("data-group")) >= 0) {
                g.classList.add("is-open");
            }
            var toggle = g.querySelector(".tis-nav__toggle");
            if (toggle) toggle.setAttribute("aria-expanded", g.classList.contains("is-open") ? "true" : "false");
        });
        each(doc.querySelectorAll(".tis-nav__link, .tis-nav__toggle"), function (el) {
            var label = el.querySelector(".tis-nav__label");
            if (label && !el.getAttribute("title")) {
                var text = label.textContent.trim();
                el.setAttribute("data-tip", text);
                el.setAttribute("title", text);
            }
        });
        var active = doc.querySelector(".tis-nav .tis-nav__link.is-active");
        if (active && active.scrollIntoView) {
            var nav = doc.querySelector(".tis-nav");
            if (nav && active.offsetTop > nav.clientHeight - 60) nav.scrollTop = active.offsetTop - nav.clientHeight / 2;
        }
    }

    var flyout = null;

    function closeFlyout() {
        if (flyout && flyout.parentNode) flyout.parentNode.removeChild(flyout);
        flyout = null;
    }

    function showFlyout(group, toggle) {
        closeFlyout();
        var items = group.querySelector(".tis-nav__items");
        if (!items) return;
        flyout = doc.createElement("div");
        flyout.className = "tis-flyout";
        flyout.setAttribute("role", "menu");
        var title = toggle.querySelector(".tis-nav__label");
        flyout.innerHTML = '<div class="tis-flyout__title">' + escapeHtml(title ? title.textContent : "") + "</div>" + items.innerHTML;
        doc.body.appendChild(flyout);
        var rect = toggle.getBoundingClientRect();
        var top = Math.min(rect.top, window.innerHeight - flyout.offsetHeight - 12);
        flyout.style.left = (rect.right + 8) + "px";
        flyout.style.top = Math.max(12, top) + "px";
    }

    function onNavToggle(toggle) {
        var group = closest(toggle, ".tis-nav__group");
        if (!group) return;
        if (root.getAttribute("data-nav") === "rail" && !isMobile()) {
            if (flyout && flyout.getAttribute("data-for") === group.getAttribute("data-group")) { closeFlyout(); return; }
            showFlyout(group, toggle);
            if (flyout) flyout.setAttribute("data-for", group.getAttribute("data-group"));
            return;
        }
        var open = !group.classList.contains("is-open");
        group.classList.toggle("is-open", open);
        toggle.setAttribute("aria-expanded", open ? "true" : "false");
        saveGroups();
    }

    /* Rail tooltips */
    var tip = null;
    function showTip(el) {
        if (root.getAttribute("data-nav") !== "rail" || isMobile()) return;
        var text = el.getAttribute("data-tip");
        if (!text) return;
        hideTip();
        tip = doc.createElement("div");
        tip.className = "tis-tooltip";
        tip.textContent = text;
        doc.body.appendChild(tip);
        var r = el.getBoundingClientRect();
        tip.style.left = (r.right + 10) + "px";
        tip.style.top = (r.top + r.height / 2 - tip.offsetHeight / 2) + "px";
    }
    function hideTip() { if (tip && tip.parentNode) tip.parentNode.removeChild(tip); tip = null; }

    /* ---------------------------------------------------- command palette */

    var palette = null;

    function paletteItems() {
        var items = [];
        var seen = {};
        each(doc.querySelectorAll(".tis-sidebar a.tis-nav__link[href]"), function (a) {
            var href = a.getAttribute("href");
            if (!href || href === "#" || seen[href]) return;
            seen[href] = true;
            var group = closest(a, ".tis-nav__group");
            var groupLabel = group ? (group.querySelector(".tis-nav__toggle .tis-nav__label") || {}).textContent : "";
            var section = a.getAttribute("data-section") || "";
            var iconEl = group ? group.querySelector(".tis-nav__toggle use") : a.querySelector("use");
            var iconName = iconEl ? (iconEl.getAttribute("href") || "").split("#i-")[1] : "file";
            items.push({
                label: (a.querySelector(".tis-nav__label") || a).textContent.trim(),
                group: (groupLabel || "Pages").trim(),
                hint: section,
                href: href,
                icon: iconName || "file"
            });
        });
        items.push({ label: "Toggle dark mode", group: "Actions", icon: "moon", run: toggleTheme });
        items.push({ label: "Collapse or expand the sidebar", group: "Actions", icon: "panel-left", run: toggleRail });
        var logout = doc.querySelector("[data-tis-logout]");
        if (logout) items.push({ label: "Sign out", group: "Actions", icon: "logout", run: function () { logout.click(); } });
        return items;
    }

    function openPalette() {
        if (palette) return;
        var all = paletteItems();
        var backdrop = doc.createElement("div");
        backdrop.className = "tis-dialog-backdrop";
        backdrop.innerHTML =
            '<div class="tis-palette" role="dialog" aria-modal="true" aria-label="Search pages and actions">' +
            '<div class="tis-palette__search">' + icon("search", "tis-icon--lg") +
            '<input type="text" placeholder="Search pages and actions..." aria-label="Search" autocomplete="off" spellcheck="false" /></div>' +
            '<ul class="tis-palette__list" role="listbox"></ul>' +
            '<div class="tis-palette__foot"><span><span class="tis-kbd">&uarr;</span> <span class="tis-kbd">&darr;</span> to move</span>' +
            '<span><span class="tis-kbd">Enter</span> to open</span><span><span class="tis-kbd">Esc</span> to close</span></div></div>';
        doc.body.appendChild(backdrop);
        palette = { el: backdrop, input: backdrop.querySelector("input"), list: backdrop.querySelector("ul"), items: all, shown: [], index: 0 };

        function render() {
            var q = palette.input.value.toLowerCase().trim();
            var terms = q ? q.split(/\s+/) : [];
            palette.shown = all.filter(function (it) {
                var hay = (it.label + " " + it.group + " " + (it.hint || "")).toLowerCase();
                for (var i = 0; i < terms.length; i++) if (hay.indexOf(terms[i]) < 0) return false;
                return true;
            });
            palette.index = Math.min(palette.index, Math.max(0, palette.shown.length - 1));
            if (!palette.shown.length) {
                palette.list.innerHTML = '<li class="tis-palette__empty">No pages or actions match "' + escapeHtml(palette.input.value) + '"</li>';
                return;
            }
            var html = "";
            var lastGroup = null;
            palette.shown.forEach(function (it, i) {
                if (it.group !== lastGroup) {
                    html += '<li class="tis-palette__group" role="presentation">' + escapeHtml(it.group) + "</li>";
                    lastGroup = it.group;
                }
                html += '<li class="tis-palette__item' + (i === palette.index ? " is-active" : "") + '" role="option" data-i="' + i + '">' +
                    icon(it.icon) + "<span>" + escapeHtml(it.label) + "</span>" + (it.hint ? "<small>" + escapeHtml(it.hint) + "</small>" : "") + "</li>";
            });
            palette.list.innerHTML = html;
            var active = palette.list.querySelector(".is-active");
            if (active && active.scrollIntoView) active.scrollIntoView({ block: "nearest" });
        }

        function run(i) {
            var it = palette.shown[i];
            if (!it) return;
            closePalette();
            if (it.run) it.run(); else window.location.href = it.href;
        }

        palette.input.addEventListener("input", function () { palette.index = 0; render(); });
        palette.input.addEventListener("keydown", function (e) {
            if (e.key === "ArrowDown") { e.preventDefault(); palette.index = Math.min(palette.index + 1, palette.shown.length - 1); render(); }
            else if (e.key === "ArrowUp") { e.preventDefault(); palette.index = Math.max(palette.index - 1, 0); render(); }
            else if (e.key === "Enter") { e.preventDefault(); run(palette.index); }
            else if (e.key === "Escape") { e.preventDefault(); closePalette(); }
        });
        palette.list.addEventListener("mousemove", function (e) {
            var li = closest(e.target, ".tis-palette__item");
            if (li && +li.getAttribute("data-i") !== palette.index) { palette.index = +li.getAttribute("data-i"); render(); }
        });
        palette.list.addEventListener("click", function (e) {
            var li = closest(e.target, ".tis-palette__item");
            if (li) run(+li.getAttribute("data-i"));
        });
        backdrop.addEventListener("mousedown", function (e) { if (e.target === backdrop) closePalette(); });
        render();
        palette.input.focus();
    }

    function closePalette() {
        if (!palette) return;
        palette.el.parentNode.removeChild(palette.el);
        palette = null;
    }

    /* ------------------------------------------------------------ toasts */

    function toastHost() {
        var host = doc.getElementById("tis-toasts");
        if (!host) {
            host = doc.createElement("div");
            host.id = "tis-toasts";
            host.setAttribute("aria-live", "polite");
            (doc.body || root).appendChild(host);
        }
        return host;
    }

    function classify(message) {
        var m = String(message || "").toLowerCase();
        if (/error|fail|exception|invalid|restricted|denied|not allowed|cannot|can't|incorrect|wrong/.test(m)) return "error";
        if (/success|saved|updated|deleted|inserted|uploaded|completed|created|done/.test(m)) return "success";
        if (/pls |please|select|enter |required|must|not found|no data|exist|already|mismatch|empty|missing/.test(m)) return "warning";
        return "info";
    }

    var TOAST_ICON = { success: "check", error: "alert-circle", warning: "alert", info: "info" };

    function toast(message, type, timeout) {
        message = message === undefined || message === null ? "" : String(message);
        type = type || classify(message);
        var el = doc.createElement("div");
        el.className = "tis-toast tis-toast--" + type;
        el.setAttribute("role", type === "error" ? "alert" : "status");
        el.innerHTML = '<span class="tis-toast__icon">' + icon(TOAST_ICON[type] || "info") + "</span>" +
            '<div class="tis-toast__body"></div><button type="button" class="tis-toast__close" aria-label="Dismiss">&times;</button>';
        el.querySelector(".tis-toast__body").textContent = message;
        function dismiss() {
            if (el.classList.contains("is-leaving")) return;
            el.classList.add("is-leaving");
            setTimeout(function () { if (el.parentNode) el.parentNode.removeChild(el); }, 170);
        }
        el.querySelector("button").addEventListener("click", dismiss);
        toastHost().appendChild(el);
        var ttl = typeof timeout === "number" ? timeout : (type === "error" ? 9000 : Math.min(8000, Math.max(3500, message.length * 55)));
        if (ttl > 0) setTimeout(dismiss, ttl);
        return el;
    }

    var nativeAlert = window.alert;
    window.tisToast = toast;
    window.tisNativeAlert = function () { return nativeAlert.apply(window, arguments); };
    window.alert = function (message) {
        try { toast(message); } catch (e) { nativeAlert(message); }
    };

    /* ----------------------------------------------------------- confirm */

    function confirmDialog(options, onConfirm) {
        var backdrop = doc.createElement("div");
        backdrop.className = "tis-dialog-backdrop";
        backdrop.innerHTML =
            '<div class="tis-dialog" role="alertdialog" aria-modal="true"><div class="tis-dialog__body">' +
            '<div class="tis-dialog__title"></div><div class="tis-dialog__text"></div></div>' +
            '<div class="tis-dialog__actions"><button type="button" class="tis-btn" data-act="cancel">Cancel</button>' +
            '<button type="button" class="tis-btn tis-btn--primary" data-act="ok"></button></div></div>';
        backdrop.querySelector(".tis-dialog__title").textContent = options.title || "Are you sure?";
        backdrop.querySelector(".tis-dialog__text").textContent = options.text || "";
        var ok = backdrop.querySelector('[data-act="ok"]');
        ok.textContent = options.ok || "Continue";
        if (options.danger) ok.className = "tis-btn tis-btn--danger";
        function close() { if (backdrop.parentNode) backdrop.parentNode.removeChild(backdrop); doc.removeEventListener("keydown", onKey, true); }
        function onKey(e) { if (e.key === "Escape") { e.stopPropagation(); close(); } }
        backdrop.addEventListener("click", function (e) {
            var act = e.target.getAttribute && e.target.getAttribute("data-act");
            if (e.target === backdrop || act === "cancel") close();
            if (act === "ok") { close(); onConfirm(); }
        });
        doc.addEventListener("keydown", onKey, true);
        doc.body.appendChild(backdrop);
        ok.focus();
    }

    doc.addEventListener("click", function (e) {
        var el = closest(e.target, "[data-confirm]");
        if (!el || el.getAttribute("data-confirmed") === "1") return;
        e.preventDefault();
        e.stopImmediatePropagation();
        confirmDialog({
            title: el.getAttribute("data-confirm-title") || "Are you sure?",
            text: el.getAttribute("data-confirm"),
            ok: el.getAttribute("data-confirm-ok") || el.value || el.textContent.trim() || "Continue",
            danger: el.hasAttribute("data-confirm-danger")
        }, function () {
            el.setAttribute("data-confirmed", "1");
            el.click();
            setTimeout(function () { el.removeAttribute("data-confirmed"); }, 0);
        });
    }, true);

    /* ---------------------------------------------------------- progress */

    function progress(on) {
        var bar = doc.getElementById("tis-progress");
        if (!bar) {
            bar = doc.createElement("div");
            bar.id = "tis-progress";
            bar.setAttribute("aria-hidden", "true");
            doc.body.appendChild(bar);
        }
        bar.classList.toggle("is-active", on);
    }

    function initPostbackHooks() {
        var form = doc.forms[0];
        if (form) {
            form.addEventListener("submit", function () {
                if (!form.target || form.target === "_self") setTimeout(function () { progress(true); }, 150);
            });
        }
        window.addEventListener("pageshow", function () { progress(false); });
        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            try {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(function () { progress(true); });
                prm.add_endRequest(function () { progress(false); enhance(doc); });
            } catch (e) { /* partial rendering disabled */ }
        }
    }

    /* ---------------------------------------------------------- dropzones */

    function formatSize(bytes) {
        if (!bytes && bytes !== 0) return "";
        if (bytes < 1024) return bytes + " B";
        if (bytes < 1048576) return (bytes / 1024).toFixed(0) + " KB";
        return (bytes / 1048576).toFixed(1) + " MB";
    }

    function initDropzones(scope) {
        each(scope.querySelectorAll(".tis-dropzone"), function (zone) {
            if (zone.getAttribute("data-ready")) return;
            zone.setAttribute("data-ready", "1");
            var input = zone.querySelector('input[type="file"]');
            if (!input) return;
            var title = zone.querySelector(".tis-dropzone__title");
            var hint = zone.querySelector(".tis-dropzone__hint");
            var defaults = { title: title ? title.textContent : "", hint: hint ? hint.textContent : "" };
            function update() {
                var file = input.files && input.files[0];
                zone.classList.toggle("has-file", !!file);
                if (title) title.textContent = file ? file.name : defaults.title;
                if (hint) hint.textContent = file ? formatSize(file.size) + " - ready to upload" : defaults.hint;
            }
            input.addEventListener("change", update);
            ["dragenter", "dragover"].forEach(function (t) {
                zone.addEventListener(t, function () { zone.classList.add("is-dragover"); });
            });
            ["dragleave", "drop"].forEach(function (t) {
                zone.addEventListener(t, function () { zone.classList.remove("is-dragover"); });
            });
        });
    }

    /* ------------------------------------------------------ table filter */

    function initFilters(scope) {
        each(scope.querySelectorAll("input[data-tis-filter]"), function (input) {
            if (input.getAttribute("data-ready")) return;
            input.setAttribute("data-ready", "1");
            input.addEventListener("input", function () {
                var table = doc.getElementById(input.getAttribute("data-tis-filter"));
                if (!table) {
                    var wrap = closest(input, ".tis-card") || doc;
                    table = wrap.querySelector("table.tis-table");
                }
                if (!table) return;
                var q = input.value.toLowerCase().trim();
                each(table.querySelectorAll("tr"), function (tr) {
                    if (tr.querySelector("th") || tr.classList.contains("tis-pager")) return;
                    tr.style.display = !q || tr.textContent.toLowerCase().indexOf(q) >= 0 ? "" : "none";
                });
            });
        });
    }

    function enhance(scope) {
        initDropzones(scope);
        initFilters(scope);
    }

    /* ------------------------------------------------------------- events */

    doc.addEventListener("click", function (e) {
        var t = e.target;
        if (closest(t, "[data-tis-theme-toggle]")) { e.preventDefault(); toggleTheme(); return; }
        if (closest(t, "[data-tis-rail-toggle]")) { e.preventDefault(); toggleRail(); return; }
        if (closest(t, "[data-tis-menu]")) { e.preventDefault(); openDrawer(!root.classList.contains("tis-nav-open")); return; }
        if (closest(t, "[data-tis-palette]")) { e.preventDefault(); openPalette(); return; }
        if (closest(t, ".tis-backdrop")) { openDrawer(false); return; }
        var toggle = closest(t, ".tis-nav__toggle");
        if (toggle) { e.preventDefault(); onNavToggle(toggle); return; }
        if (flyout && !closest(t, ".tis-flyout")) closeFlyout();
        each(doc.querySelectorAll("details.tis-usermenu[open]"), function (d) {
            if (!d.contains(t)) d.removeAttribute("open");
        });
    });

    doc.addEventListener("mouseover", function (e) {
        var el = closest(e.target, ".tis-sidebar [data-tip]");
        if (el) showTip(el); else hideTip();
    });

    doc.addEventListener("keydown", function (e) {
        var key = e.key;
        var inField = /^(INPUT|TEXTAREA|SELECT)$/.test((e.target && e.target.tagName) || "") || (e.target && e.target.isContentEditable);
        if ((e.ctrlKey || e.metaKey) && (key === "k" || key === "K")) { e.preventDefault(); if (palette) closePalette(); else openPalette(); return; }
        if (key === "/" && !inField && !palette) { e.preventDefault(); openPalette(); return; }
        if (key === "Escape") {
            closeFlyout();
            openDrawer(false);
            each(doc.querySelectorAll("details.tis-usermenu[open]"), function (d) { d.removeAttribute("open"); });
        }
    });

    window.addEventListener("resize", function () { closeFlyout(); hideTip(); });

    function onReady() {
        toastHost();
        initNav();
        initPostbackHooks();
        enhance(doc);
    }

    window.tis = { toast: toast, confirm: confirmDialog, openPalette: openPalette, toggleTheme: toggleTheme, toggleRail: toggleRail };

    if (doc.readyState === "loading") doc.addEventListener("DOMContentLoaded", onReady); else onReady();
})();
