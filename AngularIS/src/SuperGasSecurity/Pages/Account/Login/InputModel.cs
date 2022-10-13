// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperGasSecurity.Pages.Account.Login;

public class InputModel
{
    [Required]
    [DisplayName("Usuario")]
    public string Username { get; set; }

    [Required]
    [DisplayName("Contrase�a")]
    public string Password { get; set; }

    public bool RememberLogin { get; set; }

    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}