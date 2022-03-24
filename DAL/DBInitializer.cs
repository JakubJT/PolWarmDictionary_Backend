using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class DbInitializer
    {
        public static void Initialize(DictionaryContext context)
        {
            if (context.Words.Any())
            {
                return;
            }

            var kuba = new Author
            {
                Name = "Kuba"
            };
            var innyKuba = new Author
            {
                Name = "Innykuba"
            };
            context.AddRange(kuba, innyKuba);
            context.SaveChanges();

            var noun = new PartOfSpeech
            {
                Name = "Noun"
            };
            var liczebnik = new PartOfSpeech
            {
                Name = "Liczebnik"
            };
            context.AddRange(noun, liczebnik);
            context.SaveChanges();

            var stryjna = new Word
            {
                ContentWar = "Stryjna",
                ContentPol = "Żona styja",
                Author = kuba,
                PartOfSpeech = noun
            };

            var czlek = new Word
            {
                ContentWar = "Człek",
                ContentPol = "Człowiek",
                Author = kuba,
                PartOfSpeech = noun
            };

            var grubasa = new Word
            {
                ContentWar = "Grubasa",
                ContentPol = "Grubas",
                Author = kuba,
                PartOfSpeech = noun
            };

            var ruchelka = new Word
            {
                ContentWar = "Ruchełka",
                ContentPol = "Bukiet",
                Author = innyKuba,
                PartOfSpeech = noun
            };
            var chojina = new Word
            {
                ContentWar = "Chojina",
                ContentPol = "Sosna",
                Author = innyKuba,
                PartOfSpeech = noun
            };

            var jerzba = new Word
            {
                ContentWar = "Jerzba",
                ContentPol = "Wierzba",
                Author = kuba,
                PartOfSpeech = noun
            };
            var kruszka = new Word
            {
                ContentWar = "Kruszka",
                ContentPol = "Grusza",
                Author = innyKuba,
                PartOfSpeech = noun
            };

            var sztery = new Word
            {
                ContentWar = "Sztery",
                ContentPol = "Cztery",
                Author = kuba
            };
            context.AddRange(kruszka, jerzba, chojina, ruchelka, czlek, stryjna, grubasa);
            context.SaveChanges();



            var drzewa = new WordGroup
            {
                Name = "Drzewa",
                Words = new List<Word>() { kruszka, jerzba, chojina }
            };
            var rodzina = new WordGroup
            {
                Name = "Rodzina",
                Words = new List<Word>() { stryjna }
            };
            context.AddRange(drzewa, rodzina);

            context.SaveChanges();
        }
    }
}