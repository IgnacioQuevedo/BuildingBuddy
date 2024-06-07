import { ConstructionCompany } from "../../constructionCompany/interfaces/construction-company";
import { Flat } from "../../flat/interfaces/flat";
import { Manager } from "../../manager/interfaces/manager";

export interface Building 
{
    id : string,
    managerId : string,
    name : string,
    address : string,
    location : Location,
    constructionCompany : ConstructionCompany,
    commonExpenses : number,
    flats : Flat[]
}