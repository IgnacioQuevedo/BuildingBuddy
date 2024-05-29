﻿using Domain.CustomExceptions;

namespace Domain;

public class Administrator : SystemUser
{
    #region Properties
    public string LastName { get; set; }
    public IEnumerable<Invitation> Invitations { get; set; } = new List<Invitation>();
    
    #endregion


    public override bool Equals(object? objectToCompare)
    {
        Administrator? administratorToCompare = objectToCompare as Administrator;

        return Id == administratorToCompare.Id && Firstname == administratorToCompare.Firstname &&
               LastName == administratorToCompare.LastName && Email == administratorToCompare.Email;
    }

    #region Validators
    
    public void AdministratorValidator()
    {
        try
        {
            PersonValidator();
            PasswordValidator();
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new InvalidAdministratorException(exceptionCaught.Message);
        }
        catch (InvalidSystemUserException exceptionCaught)
        {
            throw new InvalidAdministratorException(exceptionCaught.Message);
        }
        LastnameValidator();
    }

    private void LastnameValidator()
    {
        if (string.IsNullOrEmpty(LastName))
        {
            throw new InvalidAdministratorException("Last name is required");
        }
    }
    
    #endregion
}