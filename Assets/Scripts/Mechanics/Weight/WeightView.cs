using DG.Tweening;
using Graf.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WeightView : MonoBehaviour
{
    [SerializeField]
    private ColorPainter colorPainter;

    public bool check = true;
    public MainStayView Point { get; set; }

    private GameObject rope;
    private SpriteRenderer ropeSpriteRenderer;
    private EventManager eventManager;

    public void Init(Sprite ropeSprite, PaletteConfig colorsConfig, EventManager eventManager)
    {
        this.eventManager = eventManager;

        InitRope(ropeSprite);
        colorPainter.Init(colorsConfig);
    }

    private void OnBecameInvisible()
    {
        eventManager.SendOpenADSMenu(); // TO-DO при открытии меню черер паузу, начинает врубаться
    }
    private void InitRope(Sprite ropeSprite)
    {
        rope = new GameObject();
        rope.transform.parent = transform;
        rope.transform.localPosition = Vector3.zero;

        ropeSpriteRenderer = rope.AddComponent<SpriteRenderer>();
        ropeSpriteRenderer.sprite = ropeSprite;
        ropeSpriteRenderer.drawMode = SpriteDrawMode.Tiled;
    }
    public void ConnectStart(Transform mainStay)
    {
        Point = mainStay.GetComponent<MainStayView>();
        transform.DOScale(1, 0.5f).SetEase(Ease.OutQuint);
        rope.transform.DOScale(1, 0.5f).SetEase(Ease.OutQuint);
        DOTween.To(
            () => ropeSpriteRenderer.size,
            value => ropeSpriteRenderer.size = NormalizeRopeLength(mainStay.position, value),
            new Vector2(ropeSpriteRenderer.size.x, -Vector2.Distance(mainStay.position, transform.position)),
            0.5f
        ).SetEase(Ease.OutQuint);
        check = true;
    }
    private Vector2 NormalizeRopeLength(Vector2 targetPos, Vector2 value)
    {
        if (value.y < -Vector2.Distance(targetPos, transform.position))
            return new Vector2(ropeSpriteRenderer.size.x, -Vector2.Distance(targetPos, transform.position));

        return value;
    }

    public void ConnectEnd(Transform mainStay)
    {
        transform.parent = mainStay;
        transform.DOKill();
    }

    public void OnMainButtonClick()
    {
        transform.parent = null;
        transform.DOMove(Point.IsRight ? transform.right * 500 : transform.right * -500, 100);
        check = false;
        transform.DOScale(1.2f, 0.5f).SetEase(Ease.OutQuint);
        rope.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutQuint);
        DOTween.To(
            () => ropeSpriteRenderer.size,
            value => ropeSpriteRenderer.size = value,
            new Vector2(ropeSpriteRenderer.size.x, 0),
            0.5f
        ).SetEase(Ease.OutQuint);
    }


    private void Update()
    {
        if (check)
        {
            Vector2 dir = Point.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            ropeSpriteRenderer.size = new Vector2(ropeSpriteRenderer.size.x, -Vector2.Distance(Point.transform.position, transform.position));
        }
    }
  
    public void DestoyElement()
    {
        Destroy(this.gameObject);
    }

}
