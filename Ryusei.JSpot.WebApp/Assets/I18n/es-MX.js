i18nLocalResources["es-MX"] = {
    translation: {
        Elysium: {
            UI: {
                MsgBox: {
                    Buttons: {
                        Accept: "Aceptar",
                        Cancel: "Cancelar"
                    },
                    Titles: {
                        Info: "Informaci\xf3n",
                        Warning: "Advertencia",
                        Error: "Error",
                        Success: "Exito"
                    }
                },
                Wizard: {
                    PreviousButton: "Anterior",
                    NextButton: "Siguiente"
                },
                Notification: {
                    Info: "Información",
                    Alert: "Alerta",
                    Success: "Exito",
                    Warning: "Advertencia",
                    Error: "Error"
                }
            }
        },
        Ryusei: {
            Auth: {
                Ctrl: {
                    RegisterController: {
                        ErrorRegisterUser: "Ha ocurrido un error al registrar el usuario, por favor intentelo mas tarde",
                        ErrorValidateUser: "No ha sido posible validate tu usuario, por favor intentelo mas tarde",
                        ErrorDeactivatingUser: "No ha sido posible desactivar tu usuario, intentalo mas tarde",
                        ErrorRequestResetPassword: "No ha sido posible generar su solitud para reestablecer su contraseña, intentelo mas tarde",
                        ErrorResetPassword: "No ha sido posible reestablecer su contraseña"
                    },
                    MenuItemCtrl: {
                        ErrorInGet: "No ha sipo posible obtener el menu del usuario"
                    },
                    UserCtrl: {
                        ErrorInGetCurrentUser: "No ha sido posible obtener la informacion del usuario actual",
                        ErrorUpdatingUser: "Ha ocurrido un error al actualizar la informacion del usuario",
                        ErrorUpdatingUserPassword: "Ha ocurrido un error al actualizar la contraseña del usuario",
                        ErrorUploadingProfilePicture: "Ha ocurrido un error actualizar su foto de perfil",
                        ErrorDownloadingProfilePicture: "Ha ocurrido un error al descargar su foto de perfil"
                    },
                    AddressCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener la información"
                    },
                    EventGroupCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener la información"
                    },
                    ParticipantCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener la información",
                        ErrorCreation: "No ha sido posible su inscripción a este curso, por favor itentelo mas tarde"
                    }
                },
                Wrap: {
                    RegisterWrapper: {
                        ExceptionValidateNewUserNotFound: "No se ha encontrado la información del usuario",
                        ExceptionValidateAlreadValidated: "La cuenta ya ha sido validada previamente",
                        ExceptionInvalidCaptcha: "Se ha recibido un token de captcha invalido",
                        ExceptionInvalidResetPasswordLing: "El link para reestablecer su contraseña es invalido"
                    },
                    UserWrapper: {
                        ExceptionUserNotFound: "No se ha encontrado la información del usuario",
                        ExceptionBadCredentials: "Las credenciales recibidas son incorrectas"
                    }
                }
            },
            SessionGlobal: {
                AccessIsValid: {
                    UnexpectedError: 'No ha sido posible verificar si su cuenta tiene acceso a la aplicación, por favor intente mas tarde',
                    NotValidated: 'Antes de iniciar su cuenta debe ser validada'
                },
                Welcome: 'Bienvenido nuevamente'
            },
            Login: {
                Slogan: "La eficiencia acaba con los desechos y mejora la rentabilidad",
                Email: "Correo electronico",
                Password: "Contraseña",
                Reminder: "Recordarme",
                SignIn: "Iniciar Sesión",
                ValidEmail: "Por favor ingrese un correo electronico valido",
                RequiredEmail: "Por favor ingrese su correo electronico",
                MinLenghtEmail: "Debe ingresar al menos 10 caracteres para su correo electronico",
                MaxLenghtEmail: "La longitud maxima es de 150 caracteres para su correo electronico",
                RequiredPassword: "Es necesario ingresar una contraseña",
                MinLenghtPassword: "La longitud debe ser de 5 caracteres minimo",
                MaxLenghtPassword: "La longitud debe ser menos de 18 caracteres",
                RecoverPassword: '<i class="fa fa-lock mr-1"></i> Recuperar contraseña',
                Register: '¿No tienes una cuenta? <a href="/Auth/Register" class="text-info ml-1"><b>Registrate</b></a>',
                Error: {
                    LoginUnavailable: "En estos momentos no es posible iniciar sesión, por favor intentelo mas tarde"
                }
            },
            Register: {
                Name: "Nombre",
                RequiredName: "Debe ingresar un nombre",
                MinLenghtName: "La longitud minima del nombre es de 3 caracteres",
                MaxLenghtName: "La longitud maxima del nombre es de 100 caracteres",
                Lastname: "Apellidos",
                RequiredLastname: "Debe ingresar sus apellidos",
                MinLenghtLastname: "La longitud minima de los apellidos es de 5 caracteres",
                MaxLenghtLastname: "La longitud maxima de los apellidos es de 150 caracteres",
                Email: "Correo electronico",
                ValidEmail: "Por favor ingrese un correo electronico valido",
                RequiredEmail: "Por favor ingrese su correo electronico",
                MinLenghtEmail: "Debe ingresar al menos 10 caracteres para su correo electronico",
                MaxLenghtEmail: "La longitud maxima es de 150 caracteres para su correo electronico",
                Phone: "Telefono",
                RequiredPhone: "Debe ingresar un telefono",
                MinLenghtPhone: "La longitud minima del telefono es de 10 caracteres",
                MaxLenghtPhone: "La longitud maxima del telefono es de 40 caracteres",
                Company: "Compañia",
                RequiredCompany: "Debe ingresar una compañia",
                MinLenghtCompany: "La longitud minima de la compañia es de 5 caracteres",
                MaxLenghtCompany: "La longitud maxima de la compañia es de 150 caracteres",
                Job: "Puesto Laboral",
                RequiredJob: "Debe ingresar su puesto laboral",
                MinLenghtJob: "La longitud minima del puesto laboral es de 5 caracteres",
                MaxLenghtJob: "La longitud maxima del puesto laboral es de 100 caracteres",
                Password: "Contraseña",
                ConfirmPassword: "Confirmar contraseña",
                RequiredPassword: "Es necesario ingresar una contraseña",
                MinLenghtPassword: "La longitud debe ser de 5 caracteres minimo",
                MaxLenghtPassword: "La longitud debe ser menos de 18 caracteres",
                StrengthPassword: "La contraseña no cumple con la seguridad necesaria (Alfanúmerica, Mayusculas, Minusculas, Caracter Especial)",
                RequiredConfirmPassword: "Es necesario ingresar nuevamente la contraseña anterior",
                EqualToConfirmPassword: "Las contraseñas no coinciden",
                Save: "Registrar",
                Login: '¿Ya tienes una cuenta? <a href="/Auth/Login" class="text-info ml-1"><b>Inicia sesión</b></a>',
                Success: "Registro realizado",
                Info: "Tu registro se ha realizado correctamente, ahora puedes iniciar sesion", //"Para terminar tu registro, sigue las instrucciones dentro del correo que te hemos enviado",
                LoginReturn: "Regresar inicio de sesión",
                Error: "Ha ocurrido un error al realizar el registro, por favor intentelo mas tarde"
            },
            ValidateUser: {
                Title: "Gracias por seguir con tu registro",
                Description: "Espera estamos validando tu información",
                TitleSuccess: "Validación completa",
                DescriptionSuccess: "Tu cuenta se ha validado correctamente",
                LoginReturn: "Regresar inicio de sesión",
                UnexpectedError: "Ha ocurrido un error al validar la cuenta de usuario"
            },
            Recover: {
                Email: "Correo electronico",
                ValidEmail: "Por favor ingrese un correo electronico valido",
                RequiredEmail: "Por favor ingrese su correo electronico",
                MinLenghtEmail: "Debe ingresar al menos 5 caracteres para su correo electronico",
                MaxLenghtEmail: "La longitud maxima es de 150 caracteres para su correo electronico",
                Success: "Solicitud procesada",
                Info: "Para terminar tu registro, sigue las instrucciones dentro del correo que te hemos enviado",
                LoginReturn: "Regresar inicio de sesión",
                EmptyCaptcha: "No ha completado el captcha",
                UnexpectedError: "Ha ocurrido un error inesperado, por favor intentelo mas tarde",
                Save: "Reestablecer contraseña"
            },
            ResetPassword: {
                Password: "Contraseña",
                ConfirmPassword: "Confirmar contraseña",
                RequiredPassword: "Es necesario ingresar una contraseña",
                MinLenghtPassword: "La longitud debe ser de 5 caracteres minimo",
                MaxLenghtPassword: "La longitud debe ser menos de 18 caracteres",
                StrengthPassword: "La contraseña no cumple con la seguridad necesaria (Alfanúmerica, Mayusculas, Minusculas, Caracter Especial)",
                RequiredConfirmPassword: "Es necesario ingresar nuevamente la contraseña anterior",
                EqualToConfirmPassword: "Las contraseñas no coinciden",
                Save: "Reestablecer",
                Success: "Se ha restablecido tu contraseña",
                Info: "Ahora puedes iniciar sesión con tu nueva contraseña",
                LoginReturn: "Regresar inicio de sesión"
            }
        },
        Jspot: {
            Core: {
                Wrap: {
                    ParticipantWrap: {
                        ErrorGroupFull: "Lo sentimos el grupo esta lleno, por lo que no podemos proceder con tu inscripción",
                        ErrorInvalidUserDepartment: "No ha sido posible realizar la inscripción al grupo ya que no esta disponible para su area(s)"
                    },
                    UserDepartmentWrap: {
                        ErrorRelationAlreadyExist: "Algún area seleccionada ya se encuentra asignada"
                    },
                    AssistantWrap: {
                        ErrorEmptyOwners: "El evento debe tener al menos un organizador"
                    },
                    CarWrapper: {
                        ErrorCarIsInUse: "No puede elimar este auto ya que esta asignado a un evento que aún no ha terminado"
                    },
                    PassengerWrap: {
                        ErrorTransportFull: "No se ha podido agregar al transporte ya que este se encuentra lleno",
                        ErrorAlreadyAdded: "Ya ha seleccionado este transporte",
                        ErrorAlreadyHaveTransport: "Ya cuenta con un transporte asignado"
                    }
                },
                Ctrl: {
                    EventCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener sus eventos asociados",
                        ErrorCreatingEvent: "Ha ocurrido un error al crear el evento"
                    },
                    InvitationCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener sus invitaciones asociadas",
                        ErrorAcceptingInvitation: "Ha occurido un error al aceptar la invitación",
                        ErrorRejectingInvitation: "Ha ocurrido un error al rechazar la invitación"
                    },
                    DepartmentCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener los departamentos"
                    },
                    UserDepartmentCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener las areas relacionadas al usuario",
                        ErrorCreatingUserDepartment: "Ha ocurrido un error al asignar las áreas al usuario"
                    },
                    AssistantCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener la lista de asistentes",
                        ErrorUpdating: "Ha ocurrido un error al actualizar al asistente"
                    },
                    CarCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener los autos",
                        ErrorCreatingCar: "Ha ocurrido un error al guardar la información del auto",
                        ErrorUpdatingCar: "Ha ocurrido un error al actualizar la información del auto",
                        ErrorDeactivatingCar: "Ha ocurrido un error al desactivar el auto"
                    },
                    TransportCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener el transporte del evento",
                        ErrorCreation: "Ha ocurrido un error al agregar el transporte al evento"
                    },
                    PassengerCtrl: {
                        ErrorInGet: "Ha ocurrido un error al obtener los passajeros del transporte",
                        ErrorCreation: ""
                    }
                },
                Mgr: {
                    InvitationMgr: {
                        ErrorInvitationNotFound: "No se ha encontrado la invitación seleccionada"
                    },
                    AssistantMgr: {
                        ErrorAssistantExist: "El usuario ya esta asignado al evento",
                        ErrorAssistantNotExist: "El asistente no fue encontrado"
                    },
                    EventMgr: {
                        ErrorEventAlreadyExist: "Ya existe un evento con el mismo nombre"
                    },
                    DepartmentMgr: {
                        ErrorAlreadyExist: "Ya existe un departamento con algún nombre ingresado",
                        ErrorNamesRepeated: "Los departamentos ingresados contienen nombres repetidos"
                    },
                    EventGroupMgr: {
                        ErrorAlreadyExist: "Ya existe un grupo con el mismo nombre",
                        ErrorNamesRepeated: "Los grupod ingresados contienen nombres repetidos"
                    },
                    ParticipantMgr: {
                        ErrorAlreadyExist: "Ya se encuentra inscrito al grupo"
                    },
                    CarMgr: {
                        ErrorModelAlreadyExist: "Ya tiene asignado un auto con el mismo modelo",
                        ErrorNotExist: "No se ha encontrado el auto"
                    },
                    TransportMgr: {
                        ErrorNotFound: "No se ha encontrado el transporte",
                        ErrorAlreadyExist: "Ya ha agreagado un transporte similar para el evento"
                    }
                }
            },
            Home: {
                Session: {
                    ModalTitle: "Su sesión esta por terminar",
                    ModalDescription: "Estimado usuario su sesión esta próxima a terminar, si desea continuar dentro de la aplicación presione \"Necesito más tiempo\", de lo contrario presione \"Cerrar sesión\"",
                    ModalButtonAccept: "Necesito más tiempo",
                    ModalButtonReject: "Cerrar sesión",
                    ModalLabelError: "Ha ocurrido un error al tratar de renovar su sesión, por favor intente nuevamente, si el error continua contacte a soporte tecnico"
                },
                EditProfileA: '<i class="ti-user mr-1 ml-1"></i> Mi Perfil',
                LogoutA: '<i class="fa fa-power-off mr-1 ml-1"></i> Cerrar sesión',
                EditProfileB: '<i class="ti-user mr-1 ml-1"></i> Mi Perfil',
                LogoutB: '<i class="fa fa-power-off mr-1 ml-1"></i> Cerrar sesión',
                Title: "Inicio",
                Breadcumb1: "Inicio",
                PanelTitle: "Mis eventos e invitaciones",
                InvitationAcceptSuccess: "Tu nuevo evento a quedado agendado",
                InvitationAcceptError: "No hemos podido agregar el evento a tu agenda, por favor intentalo mas tarde",
                RejectInvitation: {
                    Title: 'Rechazar invitación',
                    Description: '¿Desea realmente rechazar la invitation a este evento?'
                },
                InvitationRejectSuccess: "Se ha rechazado la invitación",
                InvitationRejectError: "No hemos podido rechazar la invitación por favor intentalo nuevamente",
                ModalEventTitle: "Crear evento",
                EvenCreationSuccess: "Se ha creado el evento correctamente",
                DetailsDialog: {
                    Title: "Detalles del evento",
                    Description: "¿Desea ver los detalles del evento?"
                },
                Wizard: {
                    Step1Title: "Evento<br/><small class=\"text-ellipsis\">Información General</small>",
                    Step2Title: "Dirección<br/><small class=\"text-ellipsis\">¿Donde será el evento?</small>",
                    Step3Title: "Departamentos<br/><small class=\"text-ellipsis\">¿Que areás participaran?</small>",
                    Step4Title: "Grupos<br/><small class=\"text-ellipsis\">Defina grupos de participantes</small>",
                    Step5Title: "Acceso<br/><small class=\"text-ellipsis\">¿Que areás pueden participar en los grupos?</small>",
                    Step1: {
                        Name: "Nombre del evento",
                        RequiredName: "Debe ingresar un nombre al evento",
                        MinLenghtName: "La longitud minima del nombre es de 5 caracteres",
                        MaxLenghtName: "La longitud maxima del nombre es de 150 caracteres",
                        Description: "Descripción del evento",
                        RequiredDescription: "Debe ingresar una descripción al evento",
                        MinLenghtDescription: "La longitud minima del nombre es de 15 caracteres",
                        MaxLenghtDescription: "La longitud maxima del nombre es de 250 caracteres",
                        StartDate: "Fecha de inicio",
                        EndDate: "Fecha de terminación"
                    },
                    Step2: {
                        Street: "Calle",
                        RequiredStreet: "Debe ingresar una calle",
                        MinLenghtStreet: "La longitud minima de la calle es de 5 caracteres",
                        MaxLenghtStreet: "La longitud maxima de la calle es de 50 caracteres",
                        ExternalNumber: "Número exterior",
                        RequiredExternalNumber: "El número exterior es requerido",
                        MinLenghtExternalNumber: "La longitud minima del número exterior es de 1 caracter",
                        MaxLenghtExternalNumber: "La longitus maxima del número exteriro es de 10 caracteres",
                        InternalNumber: "Número interior",
                        MinLenghtInternalNumber: "La longitud minima del número interior es de 1 caracter",
                        MaxLenghtInternalNumber: "La longitus maxima del número interior es de 10 caracteres",
                        Country: "Pais",
                        RequiredCountry: "Debe seleccionar un pais",
                        State: "Estado",
                        RequiredState: "Debe seleccionar un estado",
                        City: "Ciudad",
                        RequiredCity: "Debe seleccionar una ciudad",
                        Map: "Ubicación del lugar",
                        RequiredMarker: "Debe colocar la ubicación del lugar",
                        Neighborhood: "Colonia",
                        RequiredNeighborhood: "Debe ingresar una colonia",
                        MinLenghtNeighborhood: "La longitud minima de la colonia es de 5 caracteres",
                        MaxLenghtNeighborhood: "La longitud maxima de la colonia es de 100 caractares",
                        ZipCode: "Código Postal",
                        RequiredZipCode: "Debe ingresar una código postal",
                        MinLenghtZipCode: "La longitud minima es de 5 caracteres",
                        MaxLenghtZipCode: "La longitud maxima es de 10 caractares"
                    },
                    Step3: {
                        DepartmentName: "Nombre de Area",
                        RequiredDepartmentName: "Debe ingresar un nombre de area",
                        MinLenghtDepartmentName: "La longitud minima del nombre del area es de 5 caracteres",
                        MaxLenghtDepartmentName: "La longitud maxima del nombre del area es de 50 caracteres",
                        DepartmentAtLeastOne: "Debe ingresar al menos una area"
                    },
                    Step4: {
                        EventGroupName: "Nombre del grupo",
                        RequiredEventGroupName: "Debe ingresar un nombre al grupo",
                        MinLenghtEventGroupName: "La longitud minima del nombre es de 5 caracteres",
                        MaxLenghtEventGroupName: "La longitud maxima del nombre es de 150 caracteres",
                        EventGroupDescription: "Descripción del grupo",
                        RequiredEventGroupDescription: "Debe ingresar una descripción del grupo",
                        MinLenghtEventGroupDescription: "La longitud minima de la descripción es de 15 caracteres",
                        MaxLenghtEventGroupDescription: "La longitud maxima de la descripción es de 550 caracteres",
                        EventGroupCapacity: "Capacidad del grupo",
                        RequiredEventGroupCapacity: "Debe ingresar una capacidad al grupo",
                        TypeEventGroupCapacity: "La capacidad debe ser númerica",
                        GreatherThanEventGroupCapacity: "La capacidad debe ser mayor a cero",
                        EventGroupStartDate: "Fecha de inicio",
                        RequiredEventGroupStartDate: "Debe seleccionar una fecha de inicio",
                        EventGroupEndDate: "Fecha de terminación",
                        RequiredEventGroupEndDate: "Debe seleccionar una fecha de terminación",
                        GroupAtLeastOne: "Debe ingresar al menos un grupo"
                    },
                    Step5: {
                        RequiredDepartments: "Debe asignar las areas que tendran acceso al grupo",
                        SelectAreas: "Seleccionar Areas",
                        GroupName: "Grupo",
                        CreateEvent: '<i class="fas fa-save"></i> Crear evento'
                    }
                }
            },
            App: {
                SessionGlobal: {
                    Welcome: "Bienvenid@ nuevamente",
                    AccessIsValid: {
                        UnexpectedError: 'No ha sido posible verificar si su cuenta tiene acceso a la aplicación, por favor intente mas tarde'
                    }
                }
            },
            MyProfile: {
                Title: "Mi Perfil",
                Breadcumb1: "Inicio",
                Breadcumb2: "Mi Perfil",
                Name: "Nombre",
                Lastname: "Apellidos",
                Email: "Correo electrónico",
                Phone: "Teléfono",
                Company: "Compañia",
                Job: "Puesto de trabajo",
                Password: "Contraseña",
                UpdateSuccess: "Se ha actualizado correctamente la información del usuario",
                UpdateUserError: "Ha ocurrido un error al actualizar la información, por favor intentelo mas tarde",
                UpdatePasswordSuccess: "Se ha actualizado su contraseña correctamente",
                UpdatePasswordError: "Ha ocurrido un error al actualizar su contraseña",
                CloseAccount: '<i class="fas fa-trash"></i> Cerrar cuenta',
                CloseAccountDialog: {
                    Title: "Cerrar cuenta",
                    Description: "¿Realmente desea cerrar su cuenta?"
                },
                IncorrectImageFile: "El formato seleccionado no esta permitido",
                MinimunSize: "El tamaño maximo permitido es de 10MB",
                UploadProfilePhotoSuccess: "Su foto de perfil se ha actualizado correctamente"
            }
        }
    }
}