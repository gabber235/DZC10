using UnityEngine;

public class CocktailThrowing : MonoBehaviour
{
    public delegate void OnHit();

    public float speed;
    public float maxHeight;

    private float _fraction;
    private Vector3 _startPos;

    private Transform _transform;

    public Transform Target
    {
        get => _transform;
        set
        {
            _transform = value;
            _startPos = transform.position + new Vector3(0f, 1.5f, 0f);
            _fraction = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Target == null) return;
        var targetPosition = Target.position;

        _fraction += Time.deltaTime * speed;

        var pos = Vector3.Lerp(_startPos, targetPosition, _fraction);

        var distanceCocktailToEnemy = DistanceXZ(targetPosition, pos);

        if (distanceCocktailToEnemy < 0.5f)
        {
            onHit?.Invoke();
            Destroy(gameObject);
            return;
        }

        var additionalHeight = Mathf.Sin(_fraction * Mathf.PI) * maxHeight;

        var height = Mathf.Lerp(_startPos.y, targetPosition.y, _fraction) + additionalHeight;

        transform.position = new Vector3(pos.x, height, pos.z);
    }

    public event OnHit onHit;


    private static float DistanceXZ(Vector3 start, Vector3 target)
    {
        return (target.x - start.x) * (target.x - start.x) + (target.z - start.z) * (target.z - start.z);
    }
}