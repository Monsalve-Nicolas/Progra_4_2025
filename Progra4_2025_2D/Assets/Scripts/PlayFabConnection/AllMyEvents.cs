using Unity.Services.Analytics;

//cambiamos el nombre de la clase al mismo que nuestro evento
public class MyFirstCustomEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public MyFirstCustomEvent() : base("myFirstCustomEvent")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public float MFCE_LindoFloat { set { SetParameter("MFCE_LindoFloat", value); } }

}

//cambiamos el nombre de la clase al mismo que nuestro evento
public class MySecondEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public MySecondEvent() : base("mySecondEvent")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public bool MSE_LindoBool { set { SetParameter("MSE_LindoBool", value); } }
    public string MSE_LindoString { set { SetParameter("MSE_LindoString", value); } }
    public int MSE_LindoInt { set { SetParameter("MSE_LindoInt", value); } }

}
public class EnemyAllDieEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public EnemyAllDieEvent() : base("EnemyAllDie")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public int EAD_enemiesCount { set { SetParameter("EAD_enemiesCount", value); } }
    public float EAD_time { set { SetParameter("EAD_time", value); } }

}
public class PlayerDiePosEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public PlayerDiePosEvent() : base("playerDiePos")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public float PDP_time { set { SetParameter("PDP_time", value); } }
    public float PDP_PosX { set { SetParameter("PDP_PosX", value); } }
    public float PDP_PosY { set { SetParameter("PDP_PosY", value); } }
    public float PDP_PosZ { set { SetParameter("PDP_PosZ", value); } }
}
public class ScoreAfterDieEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public ScoreAfterDieEvent() : base("scoreAfterDie")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public int SAD_score { set { SetParameter("SAD_score", value); } }

}
public class CrashTheGameEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public CrashTheGameEvent() : base("crashTheGame")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public bool CTG_crasheo { set { SetParameter("CTG_crasheo", value); } }

}
public class MostUsedPieceEvent : Event
{
    //al constructor le ponemos el mismo tipo que la clase
    public MostUsedPieceEvent() : base("mostUsedPiece")//base("siempre va el nombre del evento igual que el dasboard")
    {
    }
    //aca abajo vamos a poner las mismas variables que nuestro evento en el dashboard
    public string MUP_stringID { set { SetParameter("MUP_stringID", value); } }
    public string MUP_stringTipoPieza { set { SetParameter("MUP_stringTipoPieza", value); } }

}

