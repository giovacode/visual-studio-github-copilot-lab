using DataEntities;

namespace TinyShop.Tests;

[TestClass]
public class ProductTests
{
    [TestMethod]
    public void Product_NewInstance_HasDefaultValues()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.AreEqual(0, product.Id);
        Assert.IsNull(product.Name);
        Assert.IsNull(product.Description);
        Assert.AreEqual(0m, product.Price);
        Assert.IsNull(product.ImageUrl);
    }

    #region Id Property Tests
    [TestMethod]
    [DataRow(1)]
    [DataRow(100)]
    [DataRow(999999)]
    [DataRow(int.MaxValue)]
    public void Id_SetAndGet_StoresValue(int idValue)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = idValue;

        // Assert
        Assert.AreEqual(idValue, product.Id);
    }

    [TestMethod]
    public void Id_SetToZero_StoresZero()
    {
        // Arrange
        var product = new Product { Id = 10 };

        // Act
        product.Id = 0;

        // Assert
        Assert.AreEqual(0, product.Id);
    }

    [TestMethod]
    public void Id_SetToNegative_StoresNegativeValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = -1;

        // Assert
        Assert.AreEqual(-1, product.Id);
    }
    #endregion

    #region Name Property Tests
    [TestMethod]
    [DataRow("Tent")]
    [DataRow("Hiking Backpack")]
    [DataRow("Mountain Bike")]
    [DataRow("")]
    public void Name_SetAndGet_StoresValue(string nameValue)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = nameValue;

        // Assert
        Assert.AreEqual(nameValue, product.Name);
    }

    [TestMethod]
    public void Name_SetToNull_StoresNull()
    {
        // Arrange
        var product = new Product { Name = "Product Name" };

        // Act
        product.Name = null;

        // Assert
        Assert.IsNull(product.Name);
    }

    [TestMethod]
    public void Name_SetToLongString_StoresCompleteString()
    {
        // Arrange
        var longName = new string('A', 1000);
        var product = new Product();

        // Act
        product.Name = longName;

        // Assert
        Assert.AreEqual(longName, product.Name);
        Assert.AreEqual(1000, product.Name.Length);
    }

    [TestMethod]
    public void Name_SetToWhitespace_StoresWhitespace()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = "   ";

        // Assert
        Assert.AreEqual("   ", product.Name);
    }
    #endregion

    #region Description Property Tests
    [TestMethod]
    [DataRow("High quality tent")]
    [DataRow("Lightweight and durable backpack for outdoor enthusiasts")]
    [DataRow("Mountain Bike 29\" with full suspension")]
    [DataRow("")]
    public void Description_SetAndGet_StoresValue(string descriptionValue)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Description = descriptionValue;

        // Assert
        Assert.AreEqual(descriptionValue, product.Description);
    }

    [TestMethod]
    public void Description_SetToNull_StoresNull()
    {
        // Arrange
        var product = new Product { Description = "Some description" };

        // Act
        product.Description = null;

        // Assert
        Assert.IsNull(product.Description);
    }

    [TestMethod]
    public void Description_SetToMultilineText_StoresCompleteText()
    {
        // Arrange
        var multilineDescription = "Line 1\nLine 2\nLine 3";
        var product = new Product();

        // Act
        product.Description = multilineDescription;

        // Assert
        Assert.AreEqual(multilineDescription, product.Description);
    }

    [TestMethod]
    public void Description_SetToLongString_StoresCompleteString()
    {
        // Arrange
        var longDescription = new string('D', 5000);
        var product = new Product();

        // Act
        product.Description = longDescription;

        // Assert
        Assert.AreEqual(longDescription, product.Description);
    }
    #endregion

    #region Price Property Tests
    [TestMethod]
    [DataRow(0)]
    [DataRow(9.99)]
    [DataRow(99.99)]
    [DataRow(1000.00)]
    [DataRow(0.01)]
    public void Price_SetAndGet_StoresValue(double priceValue)
    {
        // Arrange
        var product = new Product();
        var decimalPrice = (decimal)priceValue;

        // Act
        product.Price = decimalPrice;

        // Assert
        Assert.AreEqual(decimalPrice, product.Price);
    }

    [TestMethod]
    public void Price_SetToMaxDecimal_StoresMaxValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = decimal.MaxValue;

        // Assert
        Assert.AreEqual(decimal.MaxValue, product.Price);
    }

    [TestMethod]
    public void Price_SetToMinDecimal_StoresMinValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = decimal.MinValue;

        // Assert
        Assert.AreEqual(decimal.MinValue, product.Price);
    }

    [TestMethod]
    public void Price_SetToNegative_StoresNegativeValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = -99.99m;

        // Assert
        Assert.AreEqual(-99.99m, product.Price);
    }

    [TestMethod]
    public void Price_SetToPreciseDecimal_MaintainsPrecision()
    {
        // Arrange
        var product = new Product();
        var precisePrice = 19.9999m;

        // Act
        product.Price = precisePrice;

        // Assert
        Assert.AreEqual(precisePrice, product.Price);
    }
    #endregion

    #region ImageUrl Property Tests
    [TestMethod]
    [DataRow("image.jpg")]
    [DataRow("products/tent.png")]
    [DataRow("https://example.com/image.webp")]
    [DataRow("/images/product.gif")]
    [DataRow("")]
    public void ImageUrl_SetAndGet_StoresValue(string imageUrlValue)
    {
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = imageUrlValue;

        // Assert
        Assert.AreEqual(imageUrlValue, product.ImageUrl);
    }

    [TestMethod]
    public void ImageUrl_SetToNull_StoresNull()
    {
        // Arrange
        var product = new Product { ImageUrl = "image.jpg" };

        // Act
        product.ImageUrl = null;

        // Assert
        Assert.IsNull(product.ImageUrl);
    }

    [TestMethod]
    public void ImageUrl_SetToLongPath_StoresCompletePath()
    {
        // Arrange
        var longPath = new string('a', 2000) + ".jpg";
        var product = new Product();

        // Act
        product.ImageUrl = longPath;

        // Assert
        Assert.AreEqual(longPath, product.ImageUrl);
    }

    [TestMethod]
    public void ImageUrl_SetToUriFormat_StoresCompleteUri()
    {
        // Arrange
        var uriUrl = "https://cdn.example.com/images/products/outdoor/tent-2024-v1.jpg?size=large&format=webp";
        var product = new Product();

        // Act
        product.ImageUrl = uriUrl;

        // Assert
        Assert.AreEqual(uriUrl, product.ImageUrl);
    }
    #endregion

    #region Multiple Properties Tests
    [TestMethod]
    public void Product_SetAllProperties_StoresAllValues()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = 42;
        product.Name = "Premium Tent";
        product.Description = "A high-quality tent for outdoor camping";
        product.Price = 299.99m;
        product.ImageUrl = "products/tent.jpg";

        // Assert
        Assert.AreEqual(42, product.Id);
        Assert.AreEqual("Premium Tent", product.Name);
        Assert.AreEqual("A high-quality tent for outdoor camping", product.Description);
        Assert.AreEqual(299.99m, product.Price);
        Assert.AreEqual("products/tent.jpg", product.ImageUrl);
    }

    [TestMethod]
    [DataRow(1, "Tent", "Outdoor tent", 99.99, "tent.jpg")]
    [DataRow(2, "Backpack", "Hiking backpack", 149.50, "backpack.png")]
    [DataRow(3, "Bike", "Mountain bike", 599.00, "bike.jpg")]
    public void Product_SetMultiplePropertiesWithDataRow_StoresAllValues(
        int id, string name, string description, double price, string imageUrl)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = id;
        product.Name = name;
        product.Description = description;
        product.Price = (decimal)price;
        product.ImageUrl = imageUrl;

        // Assert
        Assert.AreEqual(id, product.Id);
        Assert.AreEqual(name, product.Name);
        Assert.AreEqual(description, product.Description);
        Assert.AreEqual((decimal)price, product.Price);
        Assert.AreEqual(imageUrl, product.ImageUrl);
    }
    #endregion

    #region Edge Cases Tests
    [TestMethod]
    public void Product_SetNameToEmptyThenToValue_UpdatesCorrectly()
    {
        // Arrange
        var product = new Product { Name = "" };

        // Act
        product.Name = "New Name";

        // Assert
        Assert.AreEqual("New Name", product.Name);
    }

    [TestMethod]
    public void Product_SetPriceMultipleTimes_LastValueIsStored()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Price = 10m;
        product.Price = 20m;
        product.Price = 30m;

        // Assert
        Assert.AreEqual(30m, product.Price);
    }

    [TestMethod]
    public void Product_SetPropertiesInDifferentOrder_StoresValuesCorrectly()
    {
        // Arrange
        var product = new Product();

        // Act
        product.ImageUrl = "image.jpg";
        product.Price = 50m;
        product.Id = 5;
        product.Name = "Product";
        product.Description = "Description";

        // Assert
        Assert.AreEqual(5, product.Id);
        Assert.AreEqual("Product", product.Name);
        Assert.AreEqual("Description", product.Description);
        Assert.AreEqual(50m, product.Price);
        Assert.AreEqual("image.jpg", product.ImageUrl);
    }
    #endregion
}
