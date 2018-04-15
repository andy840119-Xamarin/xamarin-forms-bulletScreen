using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BulletScreen.Model;
using Xamarin.Forms;

namespace BulletScreen.Controls
{
    /// <summary>
    /// Bullet screen view
    /// </summary>
    public class BulletScreenView : BulletScreenView<Comment,BulletScreenText>
    {

    }

    /// <summary>
    /// Bullet screen view
    /// </summary>
    /// <typeparam name="TComment"></typeparam>
    /// <typeparam name="TText"></typeparam>
    public class BulletScreenView<TComment,TText> : Layout<TText> , IBulletScreenView<TComment> where TComment : IComment where TText : View, IBulletScreenText , new()
    {
        #region Protected Property

        protected ObservableCollection<TComment> OnTimeComments { get; set; } = new ObservableCollection<TComment>();
        protected Double LastTime { get; set; }

        #endregion

        #region Public Property

        /// <summary>
        /// Text Opcity
        /// </summary>
        public double TextOpcity
        {
            get => this.Opacity;
            set => Opacity = value;
        }

        /// <summary>
        /// Cutrrent time
        /// </summary>
        public double CurrentTime { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public ObservableCollection<TComment> Comments { get; set; } = new ObservableCollection<TComment>();

        #endregion

        #region Ctor

        public BulletScreenView()
        {
            OnTimeComments.CollectionChanged += (a, b) =>
            {
                //new items add to the screen
                foreach (TComment newComment in b.NewItems ?? new List<TComment>())
                {
                    var text = CreateText(newComment);
                    Children.Add(text);
                }

                //old items will be remove to the screen
                foreach (TComment newComment in b.OldItems ?? new List<TComment>())
                {
                    var removeresult = Children.FirstOrDefault(X => X.CommentGuid == newComment.CommentGuid);

                    if (removeresult != null)
                        Children.Remove(removeresult);
                }
            };
        }

        #endregion

        #region Function

        /// <summary>
        /// update time
        /// </summary>
        /// <param name="currentTime"></param>
        protected void UpdateTime(double currentTime)
        {
            //text will stay at the screen tor 5 second
            var fakeTime = 5000;
            var inTimeList = Comments.Where(x => x.CommentTime < currentTime && (x.CommentTime - fakeTime) > currentTime).ToList();
            var nowComment = OnTimeComments.ToList();
            
            //https://stackoverflow.com/questions/12795882/quickest-way-to-compare-two-list
            var firstNotSecond = nowComment.Except(inTimeList).ToList();
            var secondNotFirst = inTimeList.Except(nowComment).ToList();

            //add to the list
            foreach (var comment in firstNotSecond)
            {
                OnTimeComments.Add(comment);
            }

            //remove from list
            foreach (var comment in secondNotFirst)
            {
                OnTimeComments.Remove(comment);
            }

            foreach (var comment in OnTimeComments)
            {
                var text = Children.FirstOrDefault(x => x.CommentGuid == comment.CommentGuid);
                UpdateProperty(comment, text, currentTime);
            }
        }

        /// <summary>
        /// Layout child
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Override Function

        /// <summary>
        /// Create Text
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        protected virtual TText CreateText(TComment comment)
        {
            return null;
        }

        /// <summary>
        /// Update property
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="text"></param>
        /// <param name="currentTime"></param>
        protected virtual void UpdateProperty(TComment comment,TText text,double currentTime)
        {

        }

        #endregion
    }
}
