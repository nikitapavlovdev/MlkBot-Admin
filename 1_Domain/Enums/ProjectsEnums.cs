namespace MlkAdmin._1_Domain.Enums;

public enum ErrorCodes
{
    NO_ERROR = 0,
    ENTERNAL_ERROR = 1,
    VARIABLE_IS_NULL = 2,
    ROLE_ASSIGNMENT_FAILED = 3,
    ROLE_REMOVAL_FAILED = 4
}

public enum DynamicMessages
{
    ROLES_MESSAGE,
    COLOR_GUILD_NAME_MESSAGE,
    RULES_MESSAGE,
    AUTHORIZATION_MESSAGE,
}

public enum RoleType
{
    SERVER,
    CATEGORY,
    UNIQUE,
    COLOR
}

public enum ComponentType
{
    BUTTON,
    SELECTION_MENU
}

