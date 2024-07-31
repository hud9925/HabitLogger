public class Habit
{
        public int HabitId {  get; set; }
        public string? HabitName {  get; set; }
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }

    // Initializes this Habit
    public Habit(string name, DateTime date, int quantity)
    {
        
        this.HabitName = name;
        this.Date = date;
        this.Quantity = quantity;
    }

    // Show the habit's details
    public void ShowHabitDetails()
    {
        Console.WriteLine($"Habit name: {HabitName}, Habit Quantity: {Quantity}, Date: {Date}");

    }

    // Update this habit
    
    public void UpdateName(string newName)
    {
        HabitName = newName;
    }
    public void UpdateQuantity(int newQuantity)
    {
        Quantity = newQuantity;

    }
    public void UpdateDate(DateTime date)
    {
        Date = date;
    }


}
