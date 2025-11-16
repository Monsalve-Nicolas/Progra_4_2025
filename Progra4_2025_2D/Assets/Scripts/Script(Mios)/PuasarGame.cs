using UnityEngine;

public class PuasarGame : MonoBehaviour
{
    public GameObject canvasPanelStore;
    public GameObject canvasGameplayeInfo;
    public bool isGamePaused = false;
    [Tooltip("desactiva el ataque")] public AttackTurret shootingSystem;
    [Tooltip("desactiva la rotacion")] public Turret turretRotation;
    //Ustedes tienen que poner:
    //public Script_de_Enemigo script_Enemy;
    //public EnemyTower enemy_Attack;
    public void ResumeGame()
    {
        //script_Enemy.enable = true;
        //enemy_Attack.enabled = true;

        turretRotation.enabled = true; //La rotacion vuelve a funcionar
        shootingSystem.enabled = true; //La torreta vuelve a atacar
        canvasPanelStore.SetActive(false);
        canvasGameplayeInfo.SetActive(true);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void Pause()
    {
        //script_Enemy.enable = false;
        //script_Enemy_Attack.enable = false;
        //enemy_Attack.enabled = false;

        turretRotation.enabled = false; //La rotacion deja de funcionar
        shootingSystem.enabled = false; //La torreta deja de atacar
        canvasPanelStore.SetActive(true);
        canvasGameplayeInfo.SetActive(false);
        Time.timeScale = 0;
        isGamePaused = true;
    }
}
