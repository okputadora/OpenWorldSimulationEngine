public class VirtualAnimal : VirtualDestructible
{
  public AnimalData animalData;

  public VirtualAnimal(SharedAnimalData sharedAnimalData, SharedDestructibleData sharedDestructibleData) : base(sharedDestructibleData)
  {
    animalData = new AnimalData(sharedAnimalData);
  }
}