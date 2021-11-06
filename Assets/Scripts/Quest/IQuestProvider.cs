public interface IQuestProvider : IInteractable {
    bool ProvidedQuest();
    Quest GetQuest();
}