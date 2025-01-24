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
            LoadFromCookies(); // Загружаем состояние из cookies при создании сервиса
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

            // Сохраняем данные в cookies
            SaveToCookies();

            NotifyStateChanged();
        }

        public void Logout()
        {
            UserId = 0;
            AuthState = "Войти";
            UserRole = 0;

            // Удаляем cookies при выходе
            ClearCookies();

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

        private void SaveToCookies()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null && !context.Response.HasStarted)
            {
                var cookieData = JsonSerializer.Serialize(new
                {
                    UserId,
                    AuthState,
                    UserRole
                });

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7), // Срок действия cookies
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None // или Strict, в зависимости от требований
                };

                // Временно отключаем Secure для локальной разработки
                if (context.Request.IsHttps)
                {
                    cookieOptions.Secure = true;
                }
                else
                {
                    cookieOptions.Secure = false; // Отключаем Secure для localhost
                }

                context.Response.Cookies.Append("UserState", cookieData, cookieOptions);
            }
        }

        // Загрузка данных из cookies
        private void LoadFromCookies()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null && context.Request.Cookies.TryGetValue("UserState", out string? cookieValue))
            {
                try
                {
                    var state = JsonSerializer.Deserialize<UserStateCookie>(cookieValue);
                    if (state != null)
                    {
                        UserId = state.UserId;
                        AuthState = state.AuthState;
                        UserRole = state.UserRole;
                    }
                }
                catch
                {
                    // Если cookies повреждены или некорректны
                    Logout();
                }
            }
        }

        // Очистка cookies
        private void ClearCookies()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null && !context.Response.HasStarted)
            {
                context.Response.Cookies.Delete("UserState");
            }
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

