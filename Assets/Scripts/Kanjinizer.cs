using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kanjinizer
{
    static string[] zero2nine = {"〇","一","二","三","四","五","六","七","八","九"};

    public static string Kanjinize(int num, int digit){
        if(digit>9 || digit<1) return "無量大数";

        string kanji = "";

        for(int i=0; i<digit; i++){
            kanji = zero2nine[num%10] + kanji;
            num /= 10;
        }

        return kanji;
    }

    public static string Kanjinize(int num){

        string kanji = "";

        kanji = zero2nine[num%10] + kanji;
        num /= 10;

        for(int i=0; i<9; i++){
            if(num==0)break;

            kanji = zero2nine[num%10] + kanji;
            num /= 10;
        }

        return kanji;
    }
}
