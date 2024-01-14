using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UiStatic
{
    private static string[] textNamesStatic;//静态的标识数组

    public static string[] TextNamesStatic
    {
        get { return textNamesStatic; }
        set { textNamesStatic = value; }
    }
}
