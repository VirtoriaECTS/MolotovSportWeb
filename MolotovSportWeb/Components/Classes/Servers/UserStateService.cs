namespace MolotovSportWeb.Components.Classes.Servers

{
    public class UserStateService
    {

        // Текущее состояние авторизации
        public string AuthState { get; private set; } = "Войти";

        // Событие для уведомления об изменении состояния
        public event Action? OnChange;

        public void Login()
        {
            AuthState = "Выйти";
            NotifyStateChanged();
        }

        public void Logout()
        {
            AuthState = "Войти";
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public bool cheakAuthState()
        {
            if(AuthState == "Выйти") return true;
            else return false;
        }
    }
}
