global using Logus.HMS.Messages.Booking;
global using Logus.HMS.Messages.Invoice;
global using Logus.HMS.Messages.Shared;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using NServiceBus;
global using SyncHms.Bus.Events;
global using SyncHms.Cache;
global using SyncHms.CheckDB;
global using SyncHms.Events.DependencyInjection;
global using SyncHms.Events.Extensions;
global using SyncHms.Events.Handlers.Fias;
global using SyncHms.Events.Handlers.Opera;
global using SyncHms.Events.Handlers.Fiscal;
global using SyncHms.Events.Handlers.Sanatorium;
global using SyncHms.Events.Handlers.Telegram;
global using SyncHms.Events.Infrastructure;
global using SyncHms.Events.Messages;
global using SyncHms.Fias.Entities;
global using SyncHms.Services;
global using System.Text;
global using Telegram.Bot;