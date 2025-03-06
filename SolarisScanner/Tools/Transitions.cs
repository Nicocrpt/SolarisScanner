using CommunityToolkit.Maui.Behaviors;

namespace SolarisScanner.Tools;

public class Transitions
{
    public static async Task AnimateTranslationAsync(View view, double targetX, double targetY, uint duration, Easing easing)
    {
        // Animation de translation du composant 'view' vers les coordonnées cible
        await view.TranslateTo(targetX, targetY, duration, easing);
    }

    // Animation pour le changement de couleur
    public static void AnimateBackgroundColorChange(View frame, Color startColor, Color endColor, uint duration, Easing easing)
    {
        // Animation pour la couleur de fond
        var animation = new Animation(t =>
        {
            frame.BackgroundColor = Color.FromRgba(
                Lerp(startColor.Red, endColor.Red, t),
                Lerp(startColor.Green, endColor.Green, t),
                Lerp(startColor.Blue, endColor.Blue, t),
                1);
        });

        animation.Commit(frame, "ColorAnimation", 16, duration, easing);
    }

    public static void AnimateColorChange(BoxView view, Color startColor, Color endColor, uint duration, Easing easing)
    {
        // Animation pour la couleur de fond
        var animation = new Animation(t =>
        {
            view.Color = Color.FromRgba(
                Lerp(startColor.Red, endColor.Red, t),
                Lerp(startColor.Green, endColor.Green, t),
                Lerp(startColor.Blue, endColor.Blue, t),
                1);
        });

        animation.Commit(view, "ColorAnimation", 16, duration, easing);
    }
    // Fonction d'interpolation pour les couleurs (Lerp)

    public static void AnimateOpacityChange(View view, double currentOpacity, double targetOpacity, uint duration, Easing easing)
    {
        var animation = new Animation(t =>
        {
            view.Opacity = Lerp(currentOpacity, targetOpacity, t);
        });
        animation.Commit(view, "OpacityAnimation", 16, duration, easing);
    }
    private static double Lerp(double start, double end, double t) => start + (end - start) * t;
}