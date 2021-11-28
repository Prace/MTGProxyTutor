using AutoMapper;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.Contracts.Models.Magic;
using MTGProxyTutor.Contracts.Models.Pokemon;
using MTGProxyTutor.Scryfall.Models;
using MTGProxyTutor.ViewModels;
using System.Collections.Generic;
using System.Linq;

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

					cfg.CreateMap<ScryfallCard, MagicCard>()
						.ForMember(dest => dest.CardName, src => src.MapFrom(s => s.Name))
						.ForMember(dest => dest.ManaCost, src => src.MapFrom(s => s.Mana_cost))
						.ForMember(dest => dest.Type, src => src.MapFrom(s => s.Type_line))
						.ForMember(dest => dest.Text, src => src.MapFrom(s => s.Oracle_text));

					cfg.CreateMap<ScryfallCard, MagicCardPrint>()
						.ForMember(dest => dest.SetName, src => src.MapFrom(s => s.Set_name))
						.ForMember(dest => dest.FullArt, src => src.MapFrom(s => s.Full_art))
						.ForMember(dest => dest.ImageUrls, src => src.MapFrom(s => convertScryfallImages(s)));

					#endregion

					#region DTO to ViewModel

					cfg.CreateMap<CardWrapper, CardWrapperViewModel>();
					cfg.CreateMap<CardWrapperViewModel, CardWrapper> ();

					#endregion
				});

				return config.CreateMapper();
			}
		}

		private static List<string> convertScryfallImages(ScryfallCard card) 
		{
			if (card.Image_uris == null)
			{
				return card.Card_faces == null ? new List<string> { }
					: card.Card_faces.Select(cf => cf.Image_uris.Normal).ToList();
			}
			return new List<string> { card.Image_uris.Normal };
		}
	}
}
