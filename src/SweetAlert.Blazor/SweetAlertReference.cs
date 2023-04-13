﻿using Microsoft.AspNetCore.Components;
using SweetAlert.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor
{
    public class SweetAlertReference : ISweetAlertDialogReference
    {
        public Guid Id { get; }

        public RenderFragment RenderFragment { get; private set; }
        private readonly TaskCompletionSource<DialogResult> _resultCompletion = new();
        private readonly IAlertService alertService;
        internal SweetAlertReference(Guid id, IAlertService alertService)
        {
            Id = id;
            this.alertService = alertService;           
        }

         public Task<DialogResult> Result { get=> _resultCompletion.Task; }

        public void Close()
        {
            alertService.Close(this);
        }

        public void Close(DialogResult dialogResult)
        {
            alertService.Close(this, dialogResult);
        }

        public bool Dismiss(DialogResult dialogResult)
        {
            return _resultCompletion.TrySetResult(dialogResult);
        }
        public object Dialog { get; private set; }
        public void InjectDialog(object instance)
        {
            Dialog = instance;
        }

        public void InjectRenderFragment(RenderFragment fragment)
        {
            RenderFragment = fragment;
        }
    }
}