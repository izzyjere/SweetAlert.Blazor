using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetAlert.Blazor.Services
{
    public interface IAlertService
    {
        Task Show<TComponent>(string? title, AlertOptions? options) where TComponent : IComponent;
    }
}
