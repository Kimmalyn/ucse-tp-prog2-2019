using Contratos;
using Mocks;
using MVCGrid.Models;
using MVCGrid.Web;
using Newtonsoft.Json;
using System.Linq;
using WebApp.Controllers;

namespace WebApp.Grid
{
    public static class PadresGrid
    {
        internal static void Register()
        {
            GridDefaults employeeGridDefauls = new GridDefaults()
            {
                Paging = true,
                ItemsPerPage = 10,
                Sorting = true,
                NoResultsMessage = "No hay mas usuarios para mostrar",
                NextButtonCaption = string.Empty,
                PreviousButtonCaption = string.Empty,
                SummaryMessage = "Viendo {0} a {1} de {2} registros"
            };

            MVCGridDefinitionTable.Add("PadresGrid", new MVCGridBuilder<Padre>(employeeGridDefauls)
                  .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                  .AddColumns(cols =>
                  {
                      // Add your columns here

                      cols.Add().WithColumnName("Nombre").WithHeaderText("Nombre").WithValueExpression(i => i.Nombre).WithSorting(false);
                      cols.Add().WithColumnName("Apellido").WithHeaderText("Apellido").WithValueExpression(i => i.Apellido).WithSorting(false);
                      cols.Add().WithColumnName("Email").WithHeaderText("Email").WithValueExpression(i => i.Email).WithSorting(false);
                      cols.Add().WithColumnName("Hijos").WithHeaderText("Hijos").WithValueExpression(i => i.Hijos != null ? string.Join(", ", i.Hijos.Select(x => x.Nombre)) : string.Empty).WithSorting(false);
                      cols.Add().WithColumnName("Details").WithHeaderText("").WithCellCssClassExpression((i, c) => "action").WithHtmlEncoding(false)
                                .WithValueExpression((i, c) =>
                                      string.Format("{0}{1}{2}",
                                        string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Asignar hijos'><span class='fa fa-server'></span></a>", c.UrlHelper.Action("Assign", "Padres", new { id = i.Id }), i.Id),
                                        string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Editar'><span class='fa fa-pencil'></span></a>", c.UrlHelper.Action("Form", "Padres", new { id = i.Id }), i.Id),
                                        string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Eliminar'><span class='fa fa-trash'></span></a>", c.UrlHelper.Action("Form", "Padres", new { id = i.Id, readOnly = true, delete = true }), i.Id)
                                      ));
                  })
                  .WithPaging(true, 10, true, 100)
                  .WithSorting(true, "Id")
                  .WithAdditionalQueryOptionNames("search")
                  .WithPageParameterNames("user")
                  .WithRetrieveDataMethod((context) =>
                  {
                      var options = context.QueryOptions;
                      var result = new QueryResult<Padre>();

                      string globalSearch = options.GetAdditionalQueryOptionString("search");
                      UsuarioLogueado usuarioLogueado = JsonConvert.DeserializeObject<UsuarioLogueado>(options.GetPageParameterString("user"));

                      IServicioWeb servicio = BaseController.CreateService(); //cambiar por new ImplementacionService();
                      var data = servicio.ObtenerPadres(usuarioLogueado, options.PageIndex.Value, options.ItemsPerPage.Value, globalSearch);

                      return new QueryResult<Padre>()
                      {
                          Items = data.Lista.ToArray(),
                          TotalRecords = data.CantidadRegistros,
                      };

                  })
            );
        }
    }
}
