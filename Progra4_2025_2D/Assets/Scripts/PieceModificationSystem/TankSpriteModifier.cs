using UnityEngine;

public class TankSpriteModifier : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite_Light;
    [SerializeField] SpriteRenderer sprite_TrackL;
    [SerializeField] SpriteRenderer sprite_TrackR;
    [SerializeField] SpriteRenderer sprite_Hull;
    [SerializeField] SpriteRenderer sprite_Tower;
    [SerializeField] SpriteRenderer sprite_Gun;
    [SerializeField] SpriteRenderer sprite_GunConnector;
    
    public void ChangeLightColor(Color newColor)
    {
        sprite_Light.color = newColor;
    }

    public void ChangeSprite(TankPieceType tankPieceType, Sprite newSprite)
    {
        switch (tankPieceType)
        {
            case TankPieceType.Light:
                sprite_Light.sprite = newSprite;
                break;
            case TankPieceType.Track:
                sprite_TrackL.sprite = newSprite;
                sprite_TrackR.sprite = newSprite;
                break;
            case TankPieceType.Hull:
                sprite_Hull.sprite = newSprite;
                break;
            case TankPieceType.Tower:
                sprite_Tower.sprite = newSprite;
                break;
            case TankPieceType.Gun:
                sprite_Gun.sprite = newSprite;
                break;
            case TankPieceType.GunConnector:
                sprite_GunConnector.sprite = newSprite;
                break;
        }
    }
}
