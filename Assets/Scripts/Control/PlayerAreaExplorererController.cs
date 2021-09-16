
using UnityEngine;
using UnityEngine.AI;
using Fubix.Saving;

namespace Fubix.Core
{
    public class PlayerAreaExplorererController : MonoBehaviour, ISaveable
    {
       
        [SerializeField] float maxSpeed = 4.1f;
        public bool controlsEnabled = true;
        public bool isNavMeshAgent = true;

        NavMeshAgent navMeshAgent;
        GameObject savingThingy;
        Vector3 lastSavedWorldPosition;


        // Note: this script must be on player during any scene where saving is done
        // or else players world position will be wipped clean. this is the porpose 
        // for "lastSavedWorldPosition" to deactivate the navMesh components so this 
        // script can exist in scenes where there is no navMesh like Fight Scenes.


        // Start is called before the first frame update
        void Start()
        {
            // check if there is a NavMesh
            if (isNavMeshAgent)
            {
                navMeshAgent = GetComponent<NavMeshAgent>();
                savingThingy = GameObject.FindGameObjectWithTag("Saving");

                navMeshAgent.enabled = false;
            }
     
           
        }

        // Update is called once per frame
        void Update()
        {
            // check if there is a NavMesh
            if (isNavMeshAgent)
            {
                navMeshAgent.enabled = controlsEnabled;
                if (!InteractWithMovement()) return;
                UpdateAnimator();
            }
    

        }

        public Vector3 GetWorldLocation()
        {
            return navMeshAgent.destination;
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }



        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0) && controlsEnabled)
                {
                    MoveTo(hit.point, 1f);
                }
                return true;
            }
            return false;
        }




        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 locVelocity = transform.InverseTransformDirection(velocity);
            float speed = locVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }


        public void Cancel()
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
            controlsEnabled = false;
        }

        public void StartNavMeshAgent()
        {
            navMeshAgent.enabled = true;
            navMeshAgent.isStopped = false;
            controlsEnabled = true;
        }




        public object CaptureState()
        {
            if (isNavMeshAgent)
            {
                return new SerializableVector3(transform.position);
            }
            return new SerializableVector3(lastSavedWorldPosition);
        }

        public void RestoreState(object state)
        {
            if (isNavMeshAgent)
            {
                SerializableVector3 position = (SerializableVector3)state;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position = position.ToVector();
                GetComponent<NavMeshAgent>().enabled = true;
            }
            else
            {
                print("restoring last saved position");
                SerializableVector3 position = (SerializableVector3)state;
                lastSavedWorldPosition = position.ToVector();
                
            }


        }



        void End()
        {

        }



        // after getting hit
        void Recovered()
        {


        }



        void FootL()
        {

        }
        void FootR()
        {

        }

       
    }
}