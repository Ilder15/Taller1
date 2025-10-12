using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Taller.Shared.Entities;


namespace Taller.Frontend.Components.Pages.Employees;

public partial class EmployeesForm : ComponentBase
{
    private EditContext editContext = null!;

    [EditorRequired, Parameter] public Employee Employee { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    protected override void OnInitialized()
    {
        editContext = new EditContext(Employee);
    }

    private DateTime? _fecha
    {
        get => Employee.FechaHora.Date;
        set
        {
            if (value.HasValue)
                Employee.FechaHora = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, Employee.FechaHora.Hour, Employee.FechaHora.Minute, 0);
        }
    }

    private TimeSpan? _hora
    {
        get => Employee.FechaHora.TimeOfDay;
        set
        {
            if (value.HasValue)
                Employee.FechaHora = new DateTime(Employee.FechaHora.Year, Employee.FechaHora.Month, Employee.FechaHora.Day, value.Value.Hours, value.Value.Minutes, 0);
        }
    }
}