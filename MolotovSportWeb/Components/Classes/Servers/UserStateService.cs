namespace MolotovSportWeb.Components.Classes.Servers

{
    public class UserStateService
    {

        // Текущее состояние авторизации
        public string AuthState { get; private set; } = "Войти";

        public int UserId { get; private set; } = 0;
        public int UserRole { get; private set; } = 0;


        // Событие для уведомления об изменении состояния
        public event Action? OnChange;

        public void Login(string name, int id, int role)
        {
            UserId = id;
            AuthState = name;
            UserRole = role;
            NotifyStateChanged();
        }

        public void Logout()
        {
            UserId = 0;
            AuthState = "Войти";
            NotifyStateChanged();
        }

        public int CheakUserId()
        {
            return UserId;
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public bool cheakAuthState()
        {
            if(AuthState != "Войти") return true;
            else return false;
        }
    }
}
