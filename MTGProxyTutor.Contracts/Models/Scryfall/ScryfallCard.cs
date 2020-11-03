﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Models.Scryfall
{
    public class ImageUris
    {
        public string small { get; set; }
        public string normal { get; set; }
        public string large { get; set; }
        public string png { get; set; }
        public string art_crop { get; set; }
        public string border_crop { get; set; }
    }

    public class Legalities
    {
        public string standard { get; set; }
        public string future { get; set; }
        public string historic { get; set; }
        public string pioneer { get; set; }
        public string modern { get; set; }
        public string legacy { get; set; }
        public string pauper { get; set; }
        public string vintage { get; set; }
        public string penny { get; set; }
        public string commander { get; set; }
        public string brawl { get; set; }
        public string duel { get; set; }
        public string oldschool { get; set; }
    }

    public class Preview
    {
        public string source { get; set; }
        public string source_uri { get; set; }
        public string previewed_at { get; set; }
    }

    public class Prices
    {
        public object usd { get; set; }
        public object usd_foil { get; set; }
        public string eur { get; set; }
        public object eur_foil { get; set; }
        public object tix { get; set; }
    }

    public class ScryfallCard
    {
        public string id { get; set; }
        public string oracle_id { get; set; }
        public List<object> multiverse_ids { get; set; }
        public int tcgplayer_id { get; set; }
        public int cardmarket_id { get; set; }
        public string Name { get; set; }
        public string lang { get; set; }
        public string released_at { get; set; }
        public string uri { get; set; }
        public string scryfall_uri { get; set; }
        public string layout { get; set; }
        public bool highres_image { get; set; }
        public ImageUris image_uris { get; set; }
        public string mana_cost { get; set; }
        public double cmc { get; set; }
        public string type_line { get; set; }
        public string oracle_text { get; set; }
        public List<string> colors { get; set; }
        public List<string> color_identity { get; set; }
        public List<object> keywords { get; set; }
        public Legalities legalities { get; set; }
        public List<string> games { get; set; }
        public bool reserved { get; set; }
        public bool foil { get; set; }
        public bool nonfoil { get; set; }
        public bool oversized { get; set; }
        public bool promo { get; set; }
        public bool reprint { get; set; }
        public bool variation { get; set; }
        public string set { get; set; }
        public string set_name { get; set; }
        public string set_type { get; set; }
        public string set_search_uri { get; set; }
        public string scryfall_set_uri { get; set; }
        public string rulings_uri { get; set; }
        public string prints_search_uri { get; set; }
        public string collector_number { get; set; }
        public bool digital { get; set; }
        public string rarity { get; set; }
        public string card_back_id { get; set; }
        public string artist { get; set; }
        public List<string> artist_ids { get; set; }
        public string illustration_id { get; set; }
        public string border_color { get; set; }
        public string frame { get; set; }
        public bool full_art { get; set; }
        public bool textless { get; set; }
        public bool booster { get; set; }
        public bool story_spotlight { get; set; }
        public int edhrec_rank { get; set; }
        public Prices prices { get; set; }
    }
}
