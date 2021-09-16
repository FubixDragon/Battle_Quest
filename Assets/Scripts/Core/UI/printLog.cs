using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fubix.Core
{
    public class printLog : MonoBehaviour
    {
        private int timer = 0;
        private void Update()
        {
            timer -= 1;
            if (timer <= 0)
            {
                timer = 0;
                this.GetComponent<Text>().text = "";
            }
        }
        public void consoleLog(string log)
        {
            timer = 300;
            this.GetComponent<Text>().text = log;
        }
    }
}

