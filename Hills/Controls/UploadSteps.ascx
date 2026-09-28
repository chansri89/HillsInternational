<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadSteps.ascx.cs" Inherits="Controls_UploadSteps" %>
<ol class="tis-steps" aria-label="Upload progress">
    <li class="<%= StepClass(1) %>"><span><%= Step1Html %></span></li>
    <li class="<%= StepClass(2) %>"><span><%= Step2Html %></span></li>
    <li class="<%= StepClass(3) %>"><span><%= Step3Html %></span></li>
</ol>
