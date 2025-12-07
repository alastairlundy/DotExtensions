/*
 * Copyright (c) 2025 Alastair Lundy
 * SPDX-FileCopyrightText: 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */

using Microsoft.Extensions.Configuration;

namespace AlastairLundy.DotExtensions.MsExtensions.Configurations;

/// <summary>
/// Provides extension methods for working with configurations in Microsoft.Extensions.Configuration.
/// </summary>
public static class ContainsKeyExtensions
{
    /// <param name="configuration">The configuration to search.</param>
    extension(IConfiguration configuration)
    {
        /// <summary>
        /// Determines if a configuration contains a specified key.
        /// </summary>
        /// <param name="keyName">The key name to search for.</param>
        /// <returns>True if the key is found within the configuration, false otherwise.</returns>
        public bool ContainsKey(string keyName)
        {
            IConfigurationSection config = configuration.GetSection(keyName);

            return config.Exists();
        }
    }
}
