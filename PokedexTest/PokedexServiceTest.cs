using Moq;
using Pokedex.HttpClient;
using Pokedex.Models;
using Pokedex;
using System;

namespace PokedexTest
{
    public class PokedexServiceTest
    {
        [Test]
        public async Task DamageDictionaiesAreCreatedCorrectly()
        {
            List<TypeRelations> damageRelations = new()
            {
                new TypeRelations
                {
                    NoDamageFrom=new List<NamedAPIResource> { new NamedAPIResource(){Name="canceled1"} },
                    DoubleDamageFrom=new List<NamedAPIResource> { new NamedAPIResource(){Name="quadruple"},new NamedAPIResource(){Name="double1"} },
                    DoubleDamageTo=new List<NamedAPIResource> { new NamedAPIResource(){Name="doubled1/2"} },
                    HalfDamageFrom=new List<NamedAPIResource> { new NamedAPIResource(){Name="halved1"} },
                    HalfDamageTo=new List<NamedAPIResource> { new NamedAPIResource(){Name= "between4" },new NamedAPIResource(){Name="halved"} },
                    NoDamageTo=new List<NamedAPIResource> { new NamedAPIResource(){Name="canceled1"} },



                },
                new TypeRelations
                {
                    NoDamageFrom=new List<NamedAPIResource> { new NamedAPIResource(){Name="canceled2"} },
                    DoubleDamageFrom=new List<NamedAPIResource> { new NamedAPIResource(){Name="quadruple"},new NamedAPIResource(){Name="double2"},new NamedAPIResource(){Name="canceled1"} },
                    DoubleDamageTo=new List<NamedAPIResource> { new NamedAPIResource(){Name="doubled2"},new NamedAPIResource(){Name="canceled1"} },
                    HalfDamageFrom=new List<NamedAPIResource> { new NamedAPIResource(){Name="between4"} },
                    HalfDamageTo=new List<NamedAPIResource> { new NamedAPIResource(){Name= "between4" }, new NamedAPIResource(){Name="Canceled1"},new NamedAPIResource(){Name="doubled1/2"}  },
                    NoDamageTo=new List<NamedAPIResource>()               

                }
            };

            var target = new PokedexService(null);

            (var DamageMultipliyerDiccionary, var DamageReceivedDiccionary) = target.MakeDamageDictionaries(damageRelations);
            Assert.Multiple(() =>
            {
                Assert.That(DamageMultipliyerDiccionary["canceled1"].Item1, Is.Zero);
                Assert.That(DamageMultipliyerDiccionary["between4"].Item1, Is.EqualTo(1));
                Assert.That(DamageMultipliyerDiccionary["between4"].Item2, Is.EqualTo(4));
                Assert.That(DamageMultipliyerDiccionary["doubled1/2"].Item1, Is.EqualTo(2));
                Assert.That(DamageMultipliyerDiccionary["doubled1/2"].Item2, Is.EqualTo(2));
                Assert.That(DamageMultipliyerDiccionary["halved"].Item1, Is.EqualTo(1));
                Assert.That(DamageMultipliyerDiccionary["halved"].Item2, Is.EqualTo(2));
                Assert.That(DamageMultipliyerDiccionary["doubled2"].Item1, Is.EqualTo(2));
                Assert.That(DamageMultipliyerDiccionary["doubled2"].Item2, Is.EqualTo(1));
                Assert.That(DamageReceivedDiccionary["canceled1"].Item1, Is.EqualTo(0));
                Assert.That(DamageReceivedDiccionary["canceled1"].Item2, Is.EqualTo(1));
                Assert.That(DamageReceivedDiccionary["canceled2"].Item1, Is.EqualTo(0));
                Assert.That(DamageReceivedDiccionary["canceled2"].Item2, Is.EqualTo(1));
                Assert.That(DamageReceivedDiccionary["quadruple"].Item1, Is.EqualTo(4));
                Assert.That(DamageReceivedDiccionary["quadruple"].Item2, Is.EqualTo(1));


            });
        }
    }
}
