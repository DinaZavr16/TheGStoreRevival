namespace TheGStore.BLL.Models
{
    public static class Resourses
    {
        #region ERRORS
        public const string ERROR_IsEmpty = "Поле не може бути порожнім";
        public const string ERROR_IncorectEmail = "Електронна адреси не коректна";
        public const string ERROR_IncorectPrice = "Ціна не коректна";
        public const string ERROR_GameExists = "Така гра вже існує";
        public const string ERROR_DeveloperExists = "Такий розробник вже існує";
        public const string ERROR_CountryExists = "Така країна вже існує";
        public const string ERROR_GenreExists = "Такий жанр вже існує";
        public const string ERROR_ALREADY_BOUGHT = "Ви вже придбали цей продукт";
        #endregion ERRORS

        #region Regular expresions
        public const string REG_EXP_Email = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public const string REG_EXP_Price = @"^[0-9]+(\.[0-9]{1,2})?";
        #endregion Regular expresions

        #region Query errors
        public const string ERROR_AvgPrice = "Неможливо обрахувати середню ціну, оскільки продукти відсутні.";
        public const string ERROR_CustomersNotFound = "Покупці, що задовольняють дану умову, відсутні.";
        public const string ERROR_GameNotExists = "Програмні продукти, що задовольняють дану умову, відсутні.";
        public const string ERROR_DevNotExists = "Розробники, що задовольняють дану умову, відсутні.";
        public const string ERROR_CountryNotExists = "Країни, що задовольняють дану умову, відсутні.";
        #endregion Query errors
    }
}