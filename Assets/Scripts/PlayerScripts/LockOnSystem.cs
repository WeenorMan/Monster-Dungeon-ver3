using UnityEngine;
using Unity.Cinemachine;
using System.Linq;

public class LockOnSystem : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public CinemachineTargetGroup targetGroup;
    public float lockOnRadius = 15f;
    public string enemyTag = "Enemy";
    public Transform player;

    private Transform currentTarget;
    private bool isLockedOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isLockedOn)
                LockOnToNearestEnemy();
            else
                Unlock();
        }
    }

    void LockOnToNearestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag)
            .Select(e => e.transform)
            .Where(t => Vector3.Distance(player.position, t.position) <= lockOnRadius)
            .OrderBy(t => Vector3.Distance(player.position, t.position))
            .ToArray();

        if (enemies.Length > 0)
        {
            currentTarget = enemies[0];
            isLockedOn = true;
            UpdateTargetGroup();
        }
    }

    void Unlock()
    {
        currentTarget = null;
        isLockedOn = false;
        UpdateTargetGroup();
    }

    void UpdateTargetGroup()
    {
        targetGroup.Targets.Clear(); // Clear existing targets  

        // Add player to the target group  
        targetGroup.AddMember(player, 1f, 0f);

        if (isLockedOn && currentTarget != null)
        {
            // Add the locked-on target to the target group  
            targetGroup.AddMember(currentTarget, 1f, 0f);
        }

        virtualCamera.LookAt = targetGroup.transform;
    }

    private void Start()
    {
        // Ensure the camera always looks at the group, even when not locked on  
        UpdateTargetGroup();
    }
}
