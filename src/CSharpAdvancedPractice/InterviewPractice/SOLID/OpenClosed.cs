
// open closed principle
// classes should be open for extension but closed for modification
// here we can add new discount without modifying the existing code and checkout class
public abstract class DiscountStrategy
{
    public abstract decimal ApplyDiscount(decimal amount);
}

 public class NoDiscount: DiscountStrategy
 {
     public override decimal ApplyDiscount(decimal amount)
     {
         return amount; // No discount applied
     }
 }

 public class SeasonalDiscount : DiscountStrategy
 {
     private readonly double _discountPercentage;

     public SeasonalDiscount(double discountPercentage)
     {
         _discountPercentage = discountPercentage;
     }

     public override decimal ApplyDiscount(decimal amount)
     {
         return amount - (amount * (decimal)_discountPercentage / 100);
     }
 }

public class Checkout
{
    public decimal TotalPaymentAmount(decimal amount, DiscountStrategy strategy) => strategy.ApplyDiscount(amount); 

 }
