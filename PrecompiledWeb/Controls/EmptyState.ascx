<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmptyState.ascx.cs" Inherits="Controls_EmptyState" %>
<div class="tis-empty">
    <span class="tis-empty__icon"><svg class="tis-icon" aria-hidden="true"><use href="<%= IconHref %>"></use></svg></span>
    <span class="tis-empty__title"><%= TitleHtml %></span>
    <span class="tis-empty__text"><%= TextHtml %></span>
</div>
