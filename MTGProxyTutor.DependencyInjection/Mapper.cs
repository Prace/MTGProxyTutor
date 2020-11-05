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

					cfg.CreateMap<ScryfallCard, Card>()
						.ForMember(dest => dest.CardName, src => src.MapFrom(s => s.Name))
						.ForMember(dest => dest.ManaCost, src => src.MapFrom(s => s.Mana_cost))
						.ForMember(dest => dest.Type, src => src.MapFrom(s => s.Type_line))
						.ForMember(dest => dest.Text, src => src.MapFrom(s => s.Oracle_text))
						.ForMember(dest => dest.ImageUrl, src => src.MapFrom(s => s.Image_uris.Normal));

					#endregion
				});

				return config.CreateMapper();
			}
		}
	}
}
