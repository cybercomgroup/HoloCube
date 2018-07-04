using UnityEngine;

public class SetColor : MonoBehaviour
{
    Renderer m_ObjectRenderer;
    private static string currentCol = "";

    void OnMouseDown()
    {
        m_ObjectRenderer = GetComponent<Renderer>();

        if (m_ObjectRenderer.tag == "Col")
        {
            m_ObjectRenderer.enabled = true;
            currentCol = m_ObjectRenderer.name;
        }
        else
        {
            m_ObjectRenderer.enabled = true;
            if (currentCol == "B")
                m_ObjectRenderer.material.color = UnityEngine.Color.blue;
            else if (currentCol == "G")
                m_ObjectRenderer.material.color = UnityEngine.Color.green;
            else if (currentCol == "Y")
                m_ObjectRenderer.material.color = UnityEngine.Color.yellow;
            else if (currentCol == "R")
                m_ObjectRenderer.material.color = UnityEngine.Color.red;
            else if (currentCol == "O")
                m_ObjectRenderer.material.color = new Color(1, 0.39F, 0);
            else if (currentCol == "W")
                m_ObjectRenderer.material.color = UnityEngine.Color.white;
        }
    }
}

