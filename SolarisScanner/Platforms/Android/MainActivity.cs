using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using AndroidX.Core.View;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using Color = Android.Graphics.Color;
using Android.Views;

namespace SolarisScanner;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
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
        // if ((Resources.Configuration.UiMode & UiMode.NightMask) == UiMode.NightYes)
        // {
        //      
        //     windowInsetController.AppearanceLightNavigationBars = false;
        //     windowInsetController.AppearanceLightStatusBars = false;
        //     
        // }
        // else
        // {
        //     backgroundColor = Color.Black;
        //     windowInsetController.AppearanceLightNavigationBars = true;
        //     windowInsetController.AppearanceLightStatusBars = false;
        // }

        // Appliquer la couleur de fond de la barre de navigation
        Window.SetNavigationBarColor(backgroundColor);
        Window.SetStatusBarColor(backgroundColor);
        
    }
    
    
}