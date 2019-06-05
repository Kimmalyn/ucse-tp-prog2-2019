using Contratos;
using Mocks;
using MVCGrid.Models;
using MVCGrid.Web;
using Newtonsoft.Json;
using System.Linq;
using WebApp;
using WebApp.Controllers;

namespace WebApp.Grid
{
	public static class DocentesGrid
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

			MVCGridDefinitionTable.Add("DocentesGrid", new MVCGridBuilder<Docente>(employeeGridDefauls)
				  .WithAuthorizationType(AuthorizationType.AllowAnonymous)
				  .AddColumns(cols =>
				  {
						  // Add your columns here

						  cols.Add().WithColumnName("Nombre").WithHeaderText("Nombre").WithValueExpression(i => i.Nombre).WithSorting(false);
						  cols.Add().WithColumnName("Apellido").WithHeaderText("Apellido").WithValueExpression(i => i.Apellido).WithSorting(false);
						  cols.Add().WithColumnName("Email").WithHeaderText("Email").WithValueExpression(i => i.Email).WithSorting(false);					  
						  cols.Add().WithColumnName("Salas").WithHeaderText("Salas asignadas").WithValueExpression(i => i.Salas != null ? string.Join(", ", i.Salas.Select(x=>x.Nombre)) : string.Empty ).WithSorting(false);
					  cols.Add().WithColumnName("Details").WithHeaderText("").WithCellCssClassExpression((i, c) => "action").WithHtmlEncoding(false)
								.WithValueExpression((i, c) =>
									  string.Format("{0}{1}{2}", 									  
										string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Asignar a sala'><span class='fa fa-server'></span></a>", c.UrlHelper.Action("Assign", "Docentes", new { id = i.Id }), i.Id),
										string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Editar'><span class='fa fa-pencil'></span></a>", c.UrlHelper.Action("Form", "Docentes", new { id = i.Id }), i.Id),
										string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Eliminar'><span class='fa fa-trash'></span></a>", c.UrlHelper.Action("Form", "Docentes", new { id = i.Id, readOnly = true, delete = true }), i.Id)
									  ));
				  })
				  .WithPaging(true, 10, true, 100)
				  .WithSorting(true, "Id")
				  .WithAdditionalQueryOptionNames("search")
                  .WithPageParameterNames("user")
				  .WithRetrieveDataMethod((context) =>
				  {
					  var options = context.QueryOptions;
					  var result = new QueryResult<Docente>();

                      string globalSearch = options.GetAdditionalQueryOptionString("search");
                      UsuarioLogueado usuarioLogueado = JsonConvert.DeserializeObject<UsuarioLogueado>(options.GetPageParameterString("user"));

                      IServicioWeb servicio = BaseController.CreateService();
                      var data = servicio.ObtenerDocentes(usuarioLogueado, options.PageIndex.Value, options.ItemsPerPage.Value, globalSearch);

					  return new QueryResult<Docente>()
					  {
						  Items = data.Lista.ToArray(),
						  TotalRecords = data.CantidadRegistros,
					  };

				  })
			);
		}
	}
}
