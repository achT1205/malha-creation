namespace Catalog.Domain.Models;

public class Review : Entity<ReviewId>
{
    public ProductId ProductId { get; private set; }
    public UserId ReviewerId { get; private set; } 
    public Rating Rating { get; private set; } 
    public string Comment { get; private set; }
    public DateTime DatePosted { get; private set; }
    private int _helpfulVotes;

    //public IReadOnlyCollection<HelpfulVote> HelpfulVotes => _helpfulVotes.AsReadOnly();

    // Constructor with necessary properties to create a valid review
    private Review()
    {
        
    }

    public Review(ProductId productId, UserId reviewerId, Rating rating, string comment)
    {
        ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        ReviewerId = reviewerId ?? throw new ArgumentNullException(nameof(reviewerId));
        Rating = rating ?? throw new ArgumentNullException(nameof(rating));
        Comment = comment;
        DatePosted = DateTime.UtcNow;
        _helpfulVotes = 0;
    }

    public void UpdateComment(string newComment)
    {
        Comment = newComment;
    }

    public void AddHelpfulVote()
    {
        _helpfulVotes++;
    }

    public void RemoveHelpfulVote()
    {
        if (_helpfulVotes > 0) _helpfulVotes--;
    }
}
