﻿using JetBrains.Annotations;

namespace erl.Oracle.TnsNames
{
    /// <summary>
    /// 
    /// </summary>
    [PublicAPI]
    public enum TnsNamesSource
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Given by IFileEntry in a TNS names file
        /// </summary>
        IfileEntry,
        /// <summary>
        /// Given by environment variable ORACLE_HOME
        /// </summary>
        OracleHomeEnvironmentVariable,
        /// <summary>
        /// Given by environment variable PATH
        /// </summary>
        PathEnvironmentVariable,
        /// <summary>
        /// Given by environment variable TNS_ADMIN
        /// </summary>
        TnsAdminEnvironmentVariable,
        /// <summary>
        /// Given by user custom path
        /// </summary>
        CustomPath
    }
}