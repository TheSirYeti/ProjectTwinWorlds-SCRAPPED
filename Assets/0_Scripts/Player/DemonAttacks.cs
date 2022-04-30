using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonAttacks : PlayerAttacks
{
    public PentadentCollision weapon;
    public float flightDuration;
    public override void GenerateBasicAttack()
    {
        cooldownTimer = attackCooldown;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
        
        _playerObserver.NotifySubscribers("BasicAttack");
    }

    public override void AimAbility(Transform position, InteractableObject intObj, bool isFirst)
    {
        transform.LookAt(new Vector3(position.position.x, transform.position.y, position.position.z));
        weapon.transform.position = transform.position;
        weapon.gameObject.SetActive(true);
        weapon.transform.forward = transform.position - position.position;
        
        StartCoroutine(ThrowPentadent(weapon.duration, weapon.minDistance, position, intObj, isFirst));
    }

    public override void ExecuteAbility()
    {
        if (currentObject != null && currentObject.isObjectTriggered)
        {
            currentObject.OnObjectExecute();
        }
    }
    
    InteractableObject CheckColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(weapon.transform.position, 1f, collidableLayer);

        if (colliders.Length == 1)
        {
            return colliders[0].GetComponent<InteractableObject>();
        }
        else return null;
    }

    public override void ThrowAbility(object[] parameters)
    {
        weapon.transform.SetParent(null);
        weapon.transform.position = transform.position;
        weapon.gameObject.SetActive(false);
        
        if (currentObject != null)
        {
            SoundManager.instance.PlaySound(SoundID.PENTADENT_PULL);
            if(currentObject.isObjectTriggered)
                currentObject.OnObjectEnd();
        }
        currentObject = null;
        
        EventManager.Trigger("OnPulleyStop");
        EventManager.Trigger("OnSwingStop");
        usedAbility = false;
    }

    public void AimPentadente()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, collidableLayer))
        {
            CheckForCollisions(hit.point);
        }
    }

    public void CheckForCollisions(Vector3 objectPosition)
    {
        Collider[] collisions = Physics.OverlapSphere(objectPosition, radius, collidableLayer);

        if (collisions.Length == 1)
        {
            InteractableObject intObj = collisions[0].GetComponent<InteractableObject>();

            var dirToTarget = objectPosition - transform.position;
            
            if (LayerMask.NameToLayer(intObj.layerTrigger) == gameObject.layer)
            {
                intObj.OnObjectStart();
            }
            else
            {
                EventManager.Trigger("ResetAbility");
            }
        }
    }
    
    public IEnumerator ThrowPentadent(float duration, float minDistance, Transform currentDestination, InteractableObject intObj, bool isFirst)
    {
        PlayerWorlds.instance.isShooting = true;
        float time = 0;
        while (time <= duration)
        {
            yield return new WaitForSeconds(0.001f);
            time += Time.fixedDeltaTime;
            weapon.transform.position = Vector3.Lerp(transform.position, currentDestination.position, time / duration);

            if (Vector3.Distance(weapon.transform.position, currentDestination.position) <= minDistance)
            {
                weapon.transform.SetParent(currentDestination);
                SoundManager.instance.PlaySound(SoundID.HIT_PENTADENT);

                if (!isFirst)
                {
                    currentObject = intObj;
                    currentObject.OnObjectStart();
                }
                else
                    intObj.isFirstTriggered = true;
                
                
                break;
            }

            if (!weapon.gameObject.activeSelf)
            {
                break;
            }
        }
        PlayerWorlds.instance.isShooting = false;
        yield return new WaitForSeconds(0.0001f);
    }
}
