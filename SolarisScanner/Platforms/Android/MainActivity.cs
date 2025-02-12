using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using AndroidX.Core.View;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using Color = Android.Graphics.Color;

namespace SolarisScanner;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        updateNavigationBar();
    }

    public override void OnConfigurationChanged(Configuration newConfig)
    {
        base.OnConfigurationChanged(newConfig);
        updateNavigationBar();
    }

    private void updateNavigationBar()
    {
        WindowInsetsControllerCompat windowInsetController = WindowCompat.GetInsetsController(Window, Window.DecorView);

        // Variables pour la couleur de fond et l'état des icônes
        Color backgroundColor = Color.Transparent;

        // Vérification si le mode nuit est activé
        if ((Resources.Configuration.UiMode & UiMode.NightMask) == UiMode.NightYes)
        {
            backgroundColor = Color.ParseColor("#1f1f1f"); // Fond sombre en mode nuit
            windowInsetController.AppearanceLightNavigationBars = false;
            windowInsetController.AppearanceLightStatusBars = false;
            
        }
        else
        {
            windowInsetController.AppearanceLightNavigationBars = true;
            windowInsetController.AppearanceLightStatusBars = true;
        }

        // Appliquer la couleur de fond de la barre de navigation
        Window.SetNavigationBarColor(backgroundColor);
        Window.SetStatusBarColor(backgroundColor);
        
    }
}