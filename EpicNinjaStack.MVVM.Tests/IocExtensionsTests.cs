using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using EpicNinjaStack.Client.ViewModels;
using EpicNinjaStack.Client.ViewModels.Person;
using EpicNinjaStack.DataAccess;
using EpicNinjaStack.Domain;
using NUnit.Framework;

namespace EpicNinjaStack.MVVM.Tests
{
	[TestFixture]
	public class IocExtensionsTests
	{
		[Test]
		public void container_can_resolve_personlistviewmodel()
		{
			var ass = Assembly.GetAssembly(typeof(PersonListViewModel));
			var container = new Container();
			container.Register(typeof (IRepository<Person>), typeof (TestPersonRepository));

			container = container.RegisterViewModels(ass);

			var personListVm = container.Resolve<IListViewModel<Person>>();
			Assert.IsNotNull(personListVm, "PersonListViewModel could not be resolved");
		}
	}
}
