using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;

namespace MolotovSportWeb.Components.Classes.Servers
{
    public class UserStateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserStateService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
            UserRole = 0;




            NotifyStateChanged();
        }

        public int CheakUserId()
        {
            return UserId;
        }

        public bool cheakAuthState()
        {
            if (AuthState != "Войти") return true;
            else return false;
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }




        // Вспомогательный класс для сериализации/десериализации
        private class UserStateCookie
        {
            public int UserId { get; set; }
            public string AuthState { get; set; }
            public int UserRole { get; set; }
        }



    }
}

