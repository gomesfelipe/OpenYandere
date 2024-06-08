using System.Collections;
using System.Collections.Generic;
using OpenYandere.Managers;
using UnityEngine;
namespace OpenYandere.Characters.NPC{
public static class AIBehaviourHide {
	public static Vector3 ClosestHidingPosition(Vector3 currentPosition, Vector3 otherPlayerPosition, Collider[] obstacles, int numObstacles)
	{
		Vector3 closestHidingPosition 	= currentPosition; // return currentPosition if no obstacles
		float closestHidingDistance 	= Mathf.Infinity;

		for (int i = 0; i < numObstacles; i++) {
			Vector3 vectorFromOtherPlayerToObstacle = obstacles[i].bounds.center - otherPlayerPosition;
			Vector3 vectorFromInsideObstacleToOutsideObstacle = Vector3.Project(obstacles[i].bounds.extents, vectorFromOtherPlayerToObstacle);
			Vector3 hidingPosition = obstacles[i].bounds.center + vectorFromInsideObstacleToOutsideObstacle + AIManager.Instance.m_hideDistanceFromObstacle * vectorFromInsideObstacleToOutsideObstacle.normalized;

			float distanceToHidingPosition = Vector3.Distance(currentPosition, hidingPosition);
			if (distanceToHidingPosition < closestHidingDistance) {
				closestHidingPosition = hidingPosition;
				closestHidingDistance = distanceToHidingPosition;
			}
		}
		return closestHidingPosition;
	}
}
public static class AIBehaviourFlee {

	public static Vector2 FleeFrom(Vector3 currentPosition, Vector3 targetPosition) {
		Vector3 direction =  (targetPosition - currentPosition);
		return new Vector2(direction.x, direction.z).normalized;
	}
}	
}
