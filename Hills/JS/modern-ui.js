/* ============================================================================
   Hills TIS - modern-ui.js
   Purely presentational client helpers:
     - window.alert() -> non-blocking toast (keeps all existing code-behind
       ScriptManager.RegisterStartupScript("...alert('..')..") calls working)
     - mobile off-canvas navigation toggle
     - optional loading overlay tied to ASP.NET async postbacks
   No business logic, no server calls.
   ========================================================================== */
(function () {
    "use strict";

    /* ----------------------------------------------------------- Toasts */
    function ensureToastHost() {
        var host = document.getElementById("tis-toast-host");
        if (!host) {
            host = document.createElement("div");
            host.id = "tis-toast-host";
            host.setAttribute("aria-live", "polite");
            host.setAttribute("aria-atomic", "false");
            (document.body || document.documentElement).appendChild(host);
        }
        return host;
    }

    function classifyMessage(msg) {
        var m = (msg || "").toString().toLowerCase();
        if (/success|saved|updated|deleted|inserted|completed|record|ok\b/.test(m)) return "success";
        if (/error|fail|invalid|not\s|cannot|can't|permission|wrong|exist|required|please|must|enter|select/.test(m)) return "error";
        if (/wait|processing|warning|sure|confirm/.test(m)) return "warning";
        return "info";
    }

    var ICONS = { success: "\u2713", error: "\u26A0", warning: "\u26A0", info: "\u2139" };

    function showToast(message, type, timeout) {
        if (message === undefined || message === null) message = "";
        type = type || classifyMessage(message);
        var host = ensureToastHost();

        var toast = document.createElement("div");
        toast.className = "tis-toast tis-toast--" + type;
        toast.setAttribute("role", type === "error" ? "alert" : "status");

        var icon = document.createElement("span");
        icon.className = "tis-toast__icon";
        icon.textContent = ICONS[type] || ICONS.info;

        var body = document.createElement("div");
        body.className = "tis-toast__body";
        body.textContent = message;

        var close = document.createElement("button");
        close.type = "button";
        close.className = "tis-toast__close";
        close.setAttribute("aria-label", "Dismiss");
        close.innerHTML = "&times;";

        function dismiss() {
            if (toast.classList.contains("is-hiding")) return;
            toast.classList.add("is-hiding");
            setTimeout(function () { if (toast.parentNode) toast.parentNode.removeChild(toast); }, 200);
        }
        close.addEventListener("click", dismiss);

        toast.appendChild(icon);
        toast.appendChild(body);
        toast.appendChild(close);
        host.appendChild(toast);

        var ttl = typeof timeout === "number" ? timeout : Math.min(9000, Math.max(4000, message.length * 60));
        if (ttl > 0) setTimeout(dismiss, ttl);
        return toast;
    }

    /* Expose a helper and override the blocking native alert. */
    window.tisToast = showToast;
    var nativeAlert = window.alert;
    window.tisNativeAlert = function () { return nativeAlert.apply(window, arguments); };
    window.alert = function (message) {
        try { showToast(message); }
        catch (e) { nativeAlert(message); }
    };

    /* --------------------------------------------------- Mobile nav toggle */
    function initNavToggle() {
        var toggle = document.querySelector(".tis-menu-toggle");
        var backdrop = document.querySelector(".tis-nav-backdrop");
        if (toggle) {
            toggle.addEventListener("click", function () {
                document.body.classList.toggle("tis-nav-open");
            });
        }
        if (backdrop) {
            backdrop.addEventListener("click", function () {
                document.body.classList.remove("tis-nav-open");
            });
        }
        /* Close the drawer after choosing a menu link on small screens. */
        var sidebar = document.querySelector(".tis-sidebar");
        if (sidebar) {
            sidebar.addEventListener("click", function (e) {
                if (e.target && e.target.tagName === "A" && window.matchMedia("(max-width: 1024px)").matches) {
                    document.body.classList.remove("tis-nav-open");
                }
            });
        }
    }

    /* ------------------------------------------- Loading overlay (optional) */
    function ensureLoadingOverlay() {
        var el = document.getElementById("tis-loading");
        if (!el) {
            el = document.createElement("div");
            el.id = "tis-loading";
            el.innerHTML = '<div class="tis-spinner" role="status" aria-label="Loading"></div>';
            (document.body || document.documentElement).appendChild(el);
        }
        return el;
    }

    function initAsyncLoading() {
        if (typeof Sys === "undefined" || !Sys.WebForms || !Sys.WebForms.PageRequestManager) return;
        try {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () { ensureLoadingOverlay().classList.add("is-active"); });
            prm.add_endRequest(function () { ensureLoadingOverlay().classList.remove("is-active"); });
        } catch (e) { /* no-op */ }
    }

    function onReady() {
        ensureToastHost();
        initNavToggle();
        initAsyncLoading();
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", onReady);
    } else {
        onReady();
    }
})();
