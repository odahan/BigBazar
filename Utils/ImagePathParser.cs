#nullable disable
using System.Text.RegularExpressions;

namespace BigBazar.Utils;

public class ImagePathParser
{
    // Méthode originale retournant les valeurs en string
    public static (string, string) ExtractNumbersAsString(string imagePath)
    {
        var fileName = Path.GetFileName(imagePath);
        var regex = new Regex(@"box(\d+)_(\d+)\.jpg");
        var match = regex.Match(fileName);

        return match.Success ? (match.Groups[1].Value, match.Groups[2].Value) : ("Image number xxxx not found", "Image sub number yyyy not found");
    }

    // Variante de la méthode retournant les valeurs en int
    public static (int, int) ExtractNumbersAsInt(string imagePath)
    {
        var fileName = Path.GetFileName(imagePath);
        Regex regex = new Regex(@"box(\d+)_(\d+)\.jpg");
        Match match = regex.Match(fileName);

        if (match.Success)
        {
            // Tente de convertir les valeurs extraites en int
            bool successX = int.TryParse(match.Groups[1].Value, out int numberX);
            bool successY = int.TryParse(match.Groups[2].Value, out int numberY);

            if (successX && successY)
            {
                return (numberX, numberY);
            }
        }

        // Retourne des valeurs par défaut si la conversion échoue ou si aucun numéro n'est trouvé
        return (default, default);
    }
}