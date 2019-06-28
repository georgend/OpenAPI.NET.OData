﻿// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// ------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OpenApi.OData.Common;
using Microsoft.OpenApi.OData.Edm;

namespace Microsoft.OpenApi.OData.Vocabulary.Capabilities
{
    /// <summary>
    /// Complex Type: Org.OData.Capabilities.V1.DeleteRestrictionsType
    /// </summary>
    [Term("Org.OData.Capabilities.V1.DeleteRestrictions")]
    internal class DeleteRestrictionsType : IRecord
    {
        /// <summary>
        /// Gets the Deletable value.
        /// </summary>
        public bool? Deletable { get; private set; }

        /// <summary>
        /// Gets the navigation properties which do not allow DeleteLink requests.
        /// </summary>
        public IList<string> NonDeletableNavigationProperties { get; private set; }

        /// <summary>
        /// Gets the maximum number of navigation properties that can be traversed.
        /// </summary>
        public int? MaxLevels { get; private set; }

        /// <summary>
        /// Gets the required scopes to perform the insert.
        /// </summary>
        public PermissionType Permission { get; private set; }

        /// <summary>
        /// Gets the Supported or required custom headers.
        /// </summary>
        public IList<CustomParameter> CustomHeaders { get; private set; }

        /// <summary>
        /// Gets the Supported or required custom query options.
        /// </summary>
        public IList<CustomParameter> CustomQueryOptions { get; private set; }

        /// <summary>
        /// Test the target supports delete.
        /// </summary>
        /// <returns>True/false.</returns>
        public bool IsDeletable => Deletable == null || Deletable.Value;

        /// <summary>
        /// Test the input navigation property do not allow DeleteLink requests.
        /// </summary>
        /// <param name="navigationPropertyPath">The input navigation property path.</param>
        /// <returns>True/False.</returns>
        public bool IsNonDeletableNavigationProperty(string navigationPropertyPath)
        {
            return NonDeletableNavigationProperties != null ?
                NonDeletableNavigationProperties.Any(a => a == navigationPropertyPath) :
                false;
        }

        /// <summary>
        /// Init the <see cref="DeleteRestrictionsType"/>.
        /// </summary>
        /// <param name="record">The input record.</param>
        public void Initialize(IEdmRecordExpression record)
        {
            Utils.CheckArgumentNull(record, nameof(record));

            // Deletable
            Deletable = record.GetBoolean("Deletable");

            // NonDeletableNavigationProperties
            NonDeletableNavigationProperties = record.GetCollectionPropertyPath("NonDeletableNavigationProperties");

            // MaxLevels
            MaxLevels = (int?)record.GetInteger("MaxLevels");

            // Permission
            Permission = record.GetRecord<PermissionType>("Permission");

            // CustomHeaders
            CustomHeaders = record.GetCollection<CustomParameter>("CustomHeaders");

            // CustomQueryOptions
            CustomQueryOptions = record.GetCollection<CustomParameter>("CustomQueryOptions");
        }
    }
}
