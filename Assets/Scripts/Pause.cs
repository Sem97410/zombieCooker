using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Text _resume;
    [SerializeField] private Text _quit;

    private void Start()
    {
        Button resumeButton = _resume.GetComponent<Button>();
        resumeButton.onClick.AddListener(ResumeGame);

        Button quitButton = _quit.GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);
    }

    private void ResumeGame()
    {
        ZombieEvents.onResumeGame?.Invoke();
    }
    
    private void QuitGame()
    {
        ZombieEvents.onQuitGame?.Invoke();
    }
}
