using AutoMapper;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.Contracts.Models.Scryfall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.DependencyInjection
{
	public static class Mapper
	{
		public static IMapper Configuration
		{
			get
			{
				var config = new MapperConfiguration(cfg =>
				{
					#region Scryfall

					cfg.CreateMap<ScryfallCard, Card>();

					#endregion
				});

				return config.CreateMapper();
			}
		}
	}
}
