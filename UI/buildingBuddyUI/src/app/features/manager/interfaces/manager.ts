import { SystemUserRoleEnum } from "../../invitation/interfaces/enums/system-user-role-enum";

export interface Manager
{
    id : string,
    name : string,
    email: string,
    role : SystemUserRoleEnum.Manager,
    buildings : string[],
    maintenanceRequests : string[]
};  