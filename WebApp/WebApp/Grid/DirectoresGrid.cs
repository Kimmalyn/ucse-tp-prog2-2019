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
	public static class DirectoresGrid
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

			MVCGridDefinitionTable.Add("DirectoresGrid", new MVCGridBuilder<Directora>(employeeGridDefauls)
				  .WithAuthorizationType(AuthorizationType.AllowAnonymous)
				  .AddColumns(cols =>
				  {
						  // Add your columns here

						  cols.Add().WithColumnName("Nombre").WithHeaderText("Nombre").WithValueExpression(i => i.Nombre).WithSorting(false);
						  cols.Add().WithColumnName("Apellido").WithHeaderText("Apellido").WithValueExpression(i => i.Apellido).WithSorting(false);
						  cols.Add().WithColumnName("Email").WithHeaderText("Email").WithValueExpression(i => i.Email).WithSorting(false);
					  cols.Add().WithColumnName("Cargo").WithHeaderText("Cargo").WithValueExpression(i => i.Cargo).WithSorting(false);
					  cols.Add().WithColumnName("FechaIngreso").WithHeaderText("Fecha ingreso").WithValueExpression(i => i.FechaIngreso?.ToShortDateString()).WithSorting(false);
					  cols.Add().WithColumnName("Details").WithHeaderText("").WithCellCssClassExpression((i, c) => "action").WithHtmlEncoding(false)
								.WithValueExpression((i, c) =>
									  string.Format("{0}{1}", 									  
										string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Editar'><span class='fa fa-pencil'></span></a>", c.UrlHelper.Action("Form", "Directores", new { id = i.Id }), i.Id),
										string.Format("<a data-modal='' class='text-info' href='{0}' id='{1}' title='Eliminar'><span class='fa fa-trash'></span></a>", c.UrlHelper.Action("Form", "Directores", new { id = i.Id, readOnly = true, delete = true }), i.Id)
									  ));
				  })
				  .WithPaging(true, 10, true, 100)
				  .WithSorting(true, "Id")
				  .WithAdditionalQueryOptionNames("search")
                  .WithPageParameterNames("user")
				  .WithRetrieveDataMethod((context) =>
				  {
					  var options = context.QueryOptions;
					  var result = new QueryResult<Directora>();

                      string globalSearch = options.GetAdditionalQueryOptionString("search");
                      UsuarioLogueado usuarioLogueado = JsonConvert.DeserializeObject<UsuarioLogueado>(options.GetPageParameterString("user"));

                      IServicioWeb servicio = BaseController.CreateService();
                      var data = servicio.ObtenerDirectoras(usuarioLogueado, options.PageIndex.Value, options.ItemsPerPage.Value, globalSearch);

					  return new QueryResult<Directora>()
					  {
						  Items = data.Lista.ToArray(),
						  TotalRecords = data.CantidadRegistros,
					  };

				  })
			);
		}
	}
}
