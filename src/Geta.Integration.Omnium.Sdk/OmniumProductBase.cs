namespace Geta.Integration.Omnium.Sdk
{
    public partial class OmniumProduct : IOmniumProductBase
    {
    }

    public partial class OmniumProductVariant : IOmniumProductBase
    {
    }

    public interface IOmniumProductBase
    {
        /// <summary>
        /// Required for products: Product unique ID. If your product catalog has multiple languages, the ID should be set with the following convention {ProductId}_{Language} ('12345_en').
        /// For variants, the ID will be populated from SkuID, and should not be set manually.
        /// </summary>
        string? Id { get; set; }

        /// <summary>
        /// Product unique SKU (but equal for all language versions). This should only be set for variants, or if the product itself is sellable and is stored with 'IsSku=true' and has no variants.
        /// </summary>
        string? SkuId { get; set; }

        /// <summary>
        /// EAN code
        /// </summary>
        string? Ean { get; set; }

        /// <summary>
        /// Unique product Id, without language conventions.
        /// Variants on a product should have the same value for ProductId as the product has.
        /// </summary>
        string? ProductId { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        string? Name { get; set; }

        OmniumProductGroup ProductGroup { get; set; }

        /// <summary>
        /// Date product is created
        /// </summary>
        DateTime Created { get; set; }

        /// <summary>
        /// Date product is modified
        /// </summary>
        DateTime Modified { get; set; }

        /// <summary>
        /// Date product is published
        /// </summary>
        DateTime Published { get; set; }

        /// <summary>
        /// Product activation end date. Can be null
        /// </summary>
        DateTime? StopPublished { get; set; }

        /// <summary>
        /// True if product is active. False if it should be hidden from customers.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Label to categorize or group products. In Omnium this is used to show statistics grouped by this label. (This property is unrelated to the category tree.)
        /// </summary>
        string? Catalog { get; set; }

        /// <summary>
        /// This is the main category for the product, and should be one of the CategoryIds in Categories. If Catalog is not used, this category's name will be used as grouping in statistics.
        /// </summary>
        string? MainCategoryId { get; set; }

        /// <summary>
        /// Calculated field: If MainCategoryId is given, and exists as a category in Omnium, this property will be overwritten by the name of that category.
        /// </summary>
        string? MainCategoryName { get; set; }

        /// <summary>
        /// Product categories. If category enrichment is turned on, only CategoryId are required when adding or updating products. The other information will be populated from the already existing category with the same id.
        /// </summary>
        ICollection<OmniumProductCategory>? Categories { get; set; }

        /// <summary>
        /// Product main image url. If not set, this will be populated from the product assets, using the asset which has 'IsMainImage="true"' set. If none are set, the first asset in the list is selected.
        /// </summary>
        string? MainImageUrl { get; set; }

        /// <summary>
        /// Product media files
        /// </summary>
        ICollection<OmniumAsset>? Assets { get; set; }

        OmniumSeoInfo SeoInfo { get; set; }

        /// <summary>
        /// True if product is a bundle of other products (unlike package, price is sum of components)
        /// </summary>
        bool IsBundle { get; set; }

        /// <summary>
        /// True if product is package of other products (unlike bundle, price is specified for package)
        /// </summary>
        bool IsPackage { get; set; }

        /// <summary>
        /// True if product is write protected by user. PIM integrations should not override product information.
        /// </summary>
        bool IsWriteProtected { get; set; }

        /// <summary>
        /// A list of errors (or warnings) for the product.
        /// Typically this is used in integrations where there was a problem export products. The errors are searchable and displayed in Omnium's UI
        /// </summary>
        ICollection<OmniumEntityError>? Errors { get; set; }

        /// <summary>
        /// Calculated: This is populated automatically by default based on inventory and updated by a scheduled task which updates the products every 30 minutes.
        /// </summary>
        bool IsInStock { get; set; }

        /// <summary>
        /// Calculated: Number of items available in stock. If this is a product with variants, it will be a sum of all variants.
        /// This is populated automatically by default based on inventory and updated by a scheduled task which updates the products every 30 minutes.
        /// </summary>
        decimal AvailableInventory { get; set; }

        /// <summary>
        /// Calculated: This is populated automatically by default based on inventory and updated by a scheduled task which updates the products every 30 minutes.
        /// The inventory list contains inventory information for each warehouse(store) defined in Omnium
        /// </summary>
        ICollection<OmniumProductInventory>? Inventory { get; set; }

        /// <summary>
        /// Status of current inventory, based on AvailableInventory. Could be 'OutOfStock', 'LowInStock' or 'HighInStock'
        /// </summary>
        string? InventoryStatus { get; set; }

        /// <summary>
        /// Catalog nodes/categories. Simple list of strings of category names. Consider using the Categories property instead as this also contains IDs and category metadata.
        /// </summary>
        ICollection<string>? CatalogNodes { get; set; }

        /// <summary>
        /// Product weight in kilograms (kg)
        /// </summary>
        double? Weight { get; set; }

        OmniumPrice Price { get; set; }

        /// <summary>
        /// List of all product prices - all currencies, campaigns, customer prices etc
        /// </summary>
        ICollection<OmniumPrice>? Prices { get; set; }

        /// <summary>
        /// List of properties. Key value pairs with any non strongly typed properties.
        /// </summary>
        ICollection<OmniumPropertyItem>? Properties { get; set; }

        /// <summary>
        /// List of tags.
        /// </summary>
        ICollection<string>? Tags { get; set; }

        /// <summary>
        /// List of available market IDs. If not set, the product will be available for all markets.
        /// </summary>
        ICollection<string>? MarketIds { get; set; }

        /// <summary>
        /// List of available store IDs. If not set, the product will be available for all stores.
        /// </summary>
        ICollection<string>? StoreIds { get; set; }

        /// <summary>
        /// List of available market group IDs
        /// </summary>
        ICollection<string>? MarketGroupIds { get; set; }

        /// <summary>
        /// Product language code. This should match 'ProductContentLanguage' on a stored market in Omnium. Typically the values would be 'en' (english) or 'no' (norwegian).
        /// </summary>
        string? Language { get; set; }

        /// <summary>
        /// The location of the product in the warehouse.
        /// </summary>
        string? Location { get; set; }

        /// <summary>
        /// Product color
        /// </summary>
        string? Color { get; set; }

        /// <summary>
        /// Product brand
        /// </summary>
        string? Brand { get; set; }

        /// <summary>
        /// Product size
        /// </summary>
        string? Size { get; set; }

        /// <summary>
        /// Short product description
        /// </summary>
        string? ShortDescription { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Product Specification
        /// </summary>
        string? Specification { get; set; }

        /// <summary>
        /// Calculated - Average rating for this product
        /// </summary>
        double AverageRating { get; set; }

        /// <summary>
        /// Calculated - Number of ratings for this product
        /// </summary>
        int RatingCount { get; set; }

        /// <summary>
        /// Set to true if this product should be excluded from rating emails/forms
        /// </summary>
        bool? IsNotRatable { get; set; }

        /// <summary>
        /// Set to true if this product should be excluded from promotions
        /// </summary>
        bool? ExcludeFromPromotions { get; set; }

        /// <summary>
        /// Name of main product supplier
        /// </summary>
        string? SupplierName { get; set; }

        /// <summary>
        /// Main supplier ID
        /// </summary>
        string? SupplierId { get; set; }

        /// <summary>
        /// Lead time in days
        /// </summary>
        int? LeadTime { get; set; }

        /// <summary>
        /// Supplier lead time in days
        /// </summary>
        int? SupplierLeadTime { get; set; }

        /// <summary>
        /// External product ID from supplier, used when creating purchase orders
        /// </summary>
        string? SupplierSkuId { get; set; }

        /// <summary>
        /// Product size type
        /// </summary>
        string? SizeType { get; set; }

        /// <summary>
        /// Product unit, used for describing what unit the quantities are in
        /// </summary>
        string? Unit { get; set; }

        /// <summary>
        /// List of additional units
        /// </summary>
        ICollection<OmniumProductUnit>? Units { get; set; }

        /// <summary>
        /// List of colli, used for shipment
        /// </summary>
        ICollection<OmniumProductColli>? Colli { get; set; }

        /// <summary>
        /// Related products
        /// </summary>
        ICollection<OmniumRelatedProduct>? RelatedProducts { get; set; }

        /// <summary>
        /// Product season
        /// </summary>
        string? Season { get; set; }

        /// <summary>
        /// List of product badges or labels to display on product
        /// </summary>
        ICollection<OmniumProductBadge>? Badges { get; set; }

        /// <summary>
        /// Calculated sort index
        /// </summary>
        int SortIndex { get; set; }

        /// <summary>
        /// Sort index set by API / GUI
        /// </summary>
        int SortIndexBoost { get; set; }

        /// <summary>
        /// Calculated sort index base set by Omnium services
        /// </summary>
        int SortIndexBase { get; set; }

        /// <summary>
        /// If true, the product have no reorder level, and will usually not be in stock
        /// </summary>
        bool IsBackorder { get; set; }

        /// <summary>
        /// If true, this variant should represent the product in flattened variant lists.
        /// This can also be used if you have a product catalog with multiple products with the same parent ID set, and you only wish to show one of the products in product searches. You could then set this to 'false' on the products you wish to exclude from searches,
        /// and filter on the value.
        /// </summary>
        bool IsMainProductVariant { get; set; }

        /// <summary>
        /// If true, the product is serializable, and order line should contain serial number when completed
        /// </summary>
        bool IsSerializableProduct { get; set; }

        /// <summary>
        /// Alternative product name
        /// </summary>
        string? AlternativeProductName { get; set; }

        OmniumCustomSortIndex PopularityStandardSortIndex { get; set; }

        /// <summary>
        /// Calculated: List of market specific popularity indices
        /// </summary>
        ICollection<OmniumCustomSortIndex>? PopularitySortIndices { get; set; }

        /// <summary>
        /// Product net cost
        /// </summary>
        decimal Cost { get; set; }

        /// <summary>
        /// Currency code. E.g. USD, EUR, NOK
        /// </summary>
        string? CostCurrency { get; set; }

        /// <summary>
        /// Product components (used when product is a package)
        /// </summary>
        ICollection<OmniumProductComponent>? Components { get; set; }

        /// <summary>
        /// Available product options for the product
        /// </summary>
        ICollection<OmniumProductOption>? ProductOptions { get; set; }

        /// <summary>
        /// Optional: Specifies if this product is of a specific kind.
        /// An example of this is the type "ProductOption"
        /// A Product Option is a sold as an additional item or service to an other product, and is not sold by itself.
        /// Inscriptions or other types of customization could be this sort of option.
        /// It could also be a service, such as assembling, or it could be an additional item which would not make sense so sell on it's own
        /// </summary>
        string? ProductType { get; set; }

        /// <summary>
        /// Promotions associated with the product
        /// </summary>
        ICollection<string>? PromotionIds { get; set; }

        /// <summary>
        /// List of external product IDs. Could be ids in eCommerce platform etc
        /// </summary>
        ICollection<OmniumExternalId>? ExternalIds { get; set; }

        /// <summary>
        /// Overstock locations (extra inventory locations)
        /// </summary>
        ICollection<string>? LocationOverstock { get; set; }

        /// <summary>
        /// Virtual products will not affect inventory.
        /// </summary>
        bool? IsVirtual { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        string? Gender { get; set; }

        /// <summary>
        /// Country of origin
        /// </summary>
        string? CountryOfOrigin { get; set; }

        /// <summary>
        /// Price history (lowest prices for each market)
        /// </summary>
        ICollection<OmniumPriceReference>? LowestPriceHistory { get; set; }

        /// <summary>
        /// Assortment codes for the product
        /// </summary>
        ICollection<OmniumAssortmentCode>? AssortmentCodes { get; set; }

        /// <summary>
        /// Date when the product is expected back in stock. Calculated by purchase order services, or set externally.
        /// </summary>
        DateTime? ExpectedDeliveryDate { get; set; }
    }
}
