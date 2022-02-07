using UnityEngine;

public class SpriteBrush : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] [Range(0.1f, 1)] float _radius;
    [SerializeField] ChangeableSprite _target;

    [SerializeField] BrushMode _mode = BrushMode.Destroy;

    [SerializeField] [Range(0.01f, 1)] float _updateTime;
    float _elapsedTime;

    enum BrushMode
    {
        //Create is not implemented yet!
        Create,
        Destroy
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _updateTime)
        {
            if (!_target) return;

            _elapsedTime = 0;

            switch (_mode)
            {
                case BrushMode.Create:
                    _target.Create(transform.position, _radius);
                    break;
                case BrushMode.Destroy:
                    _target.Destroy(transform.position, _radius);
                    break;
                default:
                    break;
            }
        }
    }

    public void SetTarget(ChangeableSprite target)
    {
        _target = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
        Gizmos.DrawSphere(transform.position, _radius * 0.5f);
    }
}
