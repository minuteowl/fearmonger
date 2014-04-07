using UnityEngine;
using System.Collections;

public static class ShockPhrases {
	public static string[] phrases = new string[]{
		"Alack!",
		"Alas!",
		"Ahhh!",
		"Applesauce!",
		"Argh!",
		"Bah humbug!",
		"Balderdash!",
		"Baloney!",
		"Barf!",
		"Blarg!",
		"Blimey!",
		"Bull feathers!",
		"Codswallop!",
		"Cowabunga!",
		"Criminy!",
		"Cripes!",
		"Dear me!",
		"Eeek!",
		"Egads!",
		"Fiddlesticks!",
		"Fie!",
		"Gadzooks!",
		"Golly!",
		"Golly gee!",
		"Good heavens!",
		"Great Scott!",
		"Holy mackerel!",
		"Holy smoke!",
		"Jeepers!",
		"Leaping lizards!",
		"Malarkey!",
		"My stars and garters!",
		"Nuts!",
		"Odin's beard!",
		"Oh no!",
		"Save me!",
		"Tarnations!",
		"Thor's hammer!",
		"Woe is me!",
		"Yikes!",
		"Zounds!",
	};

	public static string Phrase(){
		int index = UnityEngine.Random.Range (0,phrases.Length-1);
		return phrases[index];
	}
}
